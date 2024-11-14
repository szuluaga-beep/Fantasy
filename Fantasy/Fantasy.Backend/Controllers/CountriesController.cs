﻿using Fantasy.Backend.Data;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountriesAsync()
    {
        var countries = await _context.Countries.ToListAsync();

        return Ok(countries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryAsync(int id)
    {
        var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

        if (country == null)
        {
            return NotFound();
        }

        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountryAsync(Country country)
    {
        _context.Add(country);
        await _context.SaveChangesAsync();
        return Created("The country has been created", country);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCountryAsync(Country country)
    {

        _context.Update(country);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountryId(int id)
    {
        var country=await _context.Countries.FirstOrDefaultAsync(x=>x.Id==id);

        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}