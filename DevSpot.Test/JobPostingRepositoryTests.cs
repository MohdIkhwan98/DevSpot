using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevSpot.Test
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                //.UseInMemoryDatabase("JobPostingDb")
                .UseInMemoryDatabase($"JobPostingDb_{Guid.NewGuid()}")
                .Options;
        }

        private ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(_options);
        }
        
        // or private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            // db context
            var db = CreateDbContext();

            // job posting repository
            var repository = new JobPostingRepository(db);

            // job posting
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Desc",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Loc",
                UserId = "TestUserId"
            };

            // execute
            await repository.AddAsync(jobPosting);
            
            // result
            var result = db.JobPostings.SingleOrDefault(x => x.Title == "Test Title"); // query
            
            // assert
            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {
            // db context
            var db = CreateDbContext();

            // job posting repository
            var repository = new JobPostingRepository(db);

            // job posting
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Desc",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Loc",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync(); // save only in in-memory

            // execute and result
            var result = await repository.GetByIdAsync(jobPosting.Id);

            // assert
            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repository.GetByIdAsync(999)
            );
        }

        [Fact]
        public async Task GetAllAsync_ShoulReturnAllJobPosting()
        {
            // db context
            var db = CreateDbContext();

            // job posting repository
            var repository = new JobPostingRepository(db);

            // job posting
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Desc",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Loc",
                UserId = "TestUserId"
            };
            
            var jobPosting2 = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Desc",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Loc",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddRangeAsync(jobPosting, jobPosting2);
            await db.SaveChangesAsync(); // save only in in-memory

            // execute and result
            var result = await repository.GetAllAsync();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatejobposting()
        {
            // db context
            var db = CreateDbContext();

            // job posting repository
            var repository = new JobPostingRepository(db);

            // job posting
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Desc",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Loc",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync(); // save only in in-memory

            jobPosting.Description = "Update Test Description";

            await repository.UpdateAsync(jobPosting);


            // execute and result
            var result = db.JobPostings.Find(jobPosting.Id);

            // assert
            Assert.NotNull(result);
            Assert.Equal("Update Test Description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPosting()
        {
            // db context
            var db = CreateDbContext();

            // job posting repository
            var repository = new JobPostingRepository(db);

            // job posting
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Desc",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Loc",
                UserId = "TestUserId"
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync(); // save only in in-memory

            // execute and result
            await repository.DeleteAsync(jobPosting.Id);

            var result = db.JobPostings.Find(jobPosting.Id);

            // assert
            Assert.Null(result);
        }
    }
}
