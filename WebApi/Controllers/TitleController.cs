﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Controllers;

[ApiController]
[Route("api/titles")]
public class TitleController : BaseController
{
    private readonly ITitlteRepository _titleRepo;
    private readonly LinkGenerator _linkGenerator;

    public TitleController(ITitlteRepository titleRepo, LinkGenerator linkGenerator) : base(linkGenerator)
    {
        _titleRepo = titleRepo;
        _linkGenerator = linkGenerator;

    }


    [HttpGet(Name = nameof(GetAllTitles))]
    public async Task<IActionResult> GetAllTitles(int page = 0, int pageSize = 25)
    {
        var titles = await _titleRepo.GetAllAsync(page, pageSize);
        var total = _titleRepo.NumberOfTitles();
        var titlesDto = titles.Select(s => s.ToTitleAndPlotDto());


        object result = CreatePaging(
            nameof(GetAllTitles),
            page,
            pageSize,
            total,
            titlesDto
            );
        return Ok(result);
    }


    [HttpGet("{startyear}", Name = nameof(GetAllTitlesByDate))]
    public async Task<ActionResult> GetAllTitlesByDate([FromRoute] string startyear, int page =0 , int pageSize=25)
    {
        var titles = await _titleRepo.GetAllTitlesByDate(startyear, page, pageSize);
        
        var total = _titleRepo.NumberOfTitles();
        
        var titlesDto = titles.Select(s=>s.ToTitleAndDateDto());

        
        return Ok(titlesDto);
    }



}
