using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardRepository _dashboardRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IPhotoService _photoService;
		private readonly UserManager<AppUser> _userManager;
		public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService, UserManager<AppUser> userManager)
		{
			_dashboardRepository = dashboardRepository;
			_httpContextAccessor = httpContextAccessor;
			_photoService = photoService;
			_userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			var userClubs = await _dashboardRepository.GetAllUserClubs();
			var userRaces = await _dashboardRepository.GetAllUserRaces();

			var dashboard = new DashboardViewModel()
			{
				Races = userRaces,
				Clubs = userClubs
			};
			return View(dashboard);
		}
		public async Task<IActionResult> EditUserProfile(string id)
		{
            //var curUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            var curUserId = id;
            var user = await _dashboardRepository.GetUserById(curUserId);



            if (user == null) { View("Error"); }

			var editUserViewModel = new EditUserViewModel
			{
				Id = curUserId,
				Pace = user.Pace,
				Mileage = user.Mileage,
				ProfileImageUrl = user.ProfileImageUrl,
				State = user.State,
				City = user.City
			};

			return View(editUserViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> EditUserProfile(EditUserViewModel userVM,string id)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit");
				return View("Error");
			}
			//var curUser = await _userManager.GetUserAsync(User);
			var curUser = await _dashboardRepository.GetUserById(id);

			if (curUser != null)
			{
				if (curUser.ProfileImageUrl != null)
				{
					try
					{
						await _photoService.DeletePhotoAsync(curUser.ProfileImageUrl);
					}
					catch (Exception ex)
					{
						ModelState.AddModelError("", "Could not delete photo ");
						return View(userVM);
					}
				}
				if (userVM.Image != null)
				{
					var photoResult = await _photoService.AddPhotoAsync(userVM.Image);
                    curUser.ProfileImageUrl = photoResult.Url.ToString();
                }



				curUser.Pace = userVM.Pace;
				curUser.Mileage = userVM.Mileage;
				curUser.State = userVM.State;
				curUser.City = userVM.City;
				

				await _userManager.UpdateAsync(curUser);
				return RedirectToAction("Index");
			}
			else
			{
				return View(userVM);
			}

		}
	}
}
