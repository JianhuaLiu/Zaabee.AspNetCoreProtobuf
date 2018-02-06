using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<TestDto> Get()
        {
            return new List<TestDto>
            {
                new TestDto
                {
                    Id = Guid.NewGuid(),
                    Tag = long.MaxValue,
                    CreateTime = DateTime.Now,
                    Name = "0",
                    Enum = TestEnum.Apple,
                    Kids = new List<TestDto>
                    {
                        new TestDto
                        {
                            Id = Guid.NewGuid(),
                            Tag = long.MaxValue - 1,
                            CreateTime = DateTime.Now,
                            Name = "00",
                            Enum = TestEnum.Banana
                        },
                        new TestDto
                        {
                            Id = Guid.NewGuid(),
                            Tag = long.MaxValue - 2,
                            CreateTime = DateTime.Now,
                            Name = "01",
                            Enum = TestEnum.Pear
                        }
                    }
                },
                new TestDto
                {
                    Id = Guid.NewGuid(),
                    Tag = long.MaxValue - 3,
                    CreateTime = DateTime.Now,
                    Name = "1",
                    Enum = TestEnum.Apple,
                    Kids = new List<TestDto>
                    {
                        new TestDto
                        {
                            Id = Guid.NewGuid(),
                            Tag = long.MaxValue - 4,
                            CreateTime = DateTime.Now,
                            Name = "10",
                            Enum = TestEnum.Banana
                        },
                        new TestDto
                        {
                            Id = Guid.NewGuid(),
                            Tag = long.MaxValue - 5,
                            CreateTime = DateTime.Now,
                            Name = "11",
                            Enum = TestEnum.Pear
                        }
                    }
                }
            };
        }
    }
}