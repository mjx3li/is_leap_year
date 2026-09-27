using is_leap_year.Services;
using Microsoft.AspNetCore.Mvc;

namespace is_leap_year.Controllers;

[ApiController]
[Route("[controller]")]
public class is_leap_yearController : ControllerBase
{
    private readonly YearStorage _storage;
    public is_leap_yearController(YearStorage storage)
    {
        _storage = storage;
    }
    public static bool is_leap_year(int year)
    {
        bool leap = false;
        if (year % 4 == 0)
        {
            if (year % 100 == 0)
            {
                if (year % 400 == 0)
                    leap = true;
                else
                    leap = false;
            }
            else
                leap = true;
        }
        else
            leap = false;

        return leap;
    }
    [HttpGet("{year}")]
    public string Getis_leap_year(int year)
    {
        if(_storage.Years.Contains(year))
        {
            return $"Год {year}, високосность - {is_leap_year(year)}";
        }
        else
        {
            return $"Не проверяли год на високосность. Используйте POST";
        }
    }
    [HttpPost("{year}")]
    public string Createis_leap_year(int year)
    {
        _storage.Years.Add(year);
        return $"Добавили год {year}, високосность = {is_leap_year(year)}";
    }
    [HttpGet]
    public string Getis_leap_year()
    {
        int count = 1;
        string res = "";
        foreach(int year in _storage.Years)
        {
            res += $"{count}. Год {year}, високосность - {is_leap_year(year)}\n";
            count++;
        }
        return res;
    }
}