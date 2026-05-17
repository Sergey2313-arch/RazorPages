using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;

        public IndexModel(ContosoUniversity.Data.ContosoUniversityContext context)
        {
            _context = context;
        }

        // Свойства для хранения состояния (сортировка, фильтр и т.д.)
        public string NameSort { get; set; }
        public string CreditsSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int CurrentPageSize { get; set; }

        //  PaginatedList вместо IList
        public PaginatedList<Course> Course { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int? pageSize)
        {
            CurrentSort = sortOrder;
            // Логика переключения сортировки
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CreditsSort = sortOrder == "Credits" ? "credits_desc" : "Credits";

            // Если изменился запрос поиска — сбрасываем на 1 страницу
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
            CurrentPageSize = pageSize ?? 5; // Твоя задача №2: дефолтный размер 5

            IQueryable<Course> coursesIQ = from c in _context.Courses select c;

            // 1. Поиск (Search)
            if (!String.IsNullOrEmpty(searchString))
            {
                coursesIQ = coursesIQ.Where(s => s.Title.Contains(searchString));
            }

            // 1. Сортировка (Sort)
            switch (sortOrder)
            {
                case "name_desc":
                    coursesIQ = coursesIQ.OrderByDescending(s => s.Title);
                    break;
                case "Credits":
                    coursesIQ = coursesIQ.OrderBy(s => s.Credits);
                    break;
                case "credits_desc":
                    coursesIQ = coursesIQ.OrderByDescending(s => s.Credits);
                    break;
                default:
                    coursesIQ = coursesIQ.OrderBy(s => s.Title);
                    break;
            }

            // 1. Пагинация (Pagination)
            Course = await PaginatedList<Course>.CreateAsync(
                coursesIQ.AsNoTracking(), pageIndex ?? 1, CurrentPageSize);
        }
    }
}