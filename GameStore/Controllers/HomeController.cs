using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Game> games2 = new List<Game>();
            try
            {
                using (DBContext DB = new DBContext())
                {
                    games2 = DB.Games.ToList();
                }

                //return View(games2);
            }
            catch (Exception Ex)
            {
                return new HttpNotFoundResult(Ex.Message + " | " + Ex.TargetSite);
            }
            return View(games2);
        }

        [Authorize]
        public ActionResult UserBasket()
        {
            List<Game> games = new List<Game>();
            try
            {
                using (DBContext DB = new DBContext())
                {
                    User CurentUser = DB.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
                    if (CurentUser == null)
                        return RedirectToAction("Index");
                    DB.Entry(CurentUser).Collection(u => u.UserGames).Load();
                    games = CurentUser.UserGames;
                }

                return View(games);
            }
            catch (Exception Ex)
            {
                return new HttpNotFoundResult(Ex.Message + " | " + Ex.TargetSite);
            }
        }

        [Authorize]
        public ActionResult AddGameInBasket(int Id)
        {
            try
            {
                using (DBContext DB = new DBContext())
                {
                    Game Add = DB.Games.FirstOrDefault(n => n.Id == Id);
                    User Current = DB.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
                    DB.Entry(Current).Collection(u => u.UserGames).Load();

                    if (Current.UserGames.FirstOrDefault(x => x.Id == Add.Id) == null)
                    {
                        if (Add != null)
                        {
                            return View(Add);
                        }
                        else
                            return HttpNotFound();
                    }
                    else
                        return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [ActionName("AddGameInBasket")]
        [HttpPost]
        public ActionResult AddGameInBasket1(int id)
        {
            using (DBContext DB = new DBContext())
            {
                Game Add = DB.Games.FirstOrDefault(n => n.Id == id);
                if (Add != null)
                {
                    DB.Users.FirstOrDefault(x => x.Login == User.Identity.Name).UserGames.Add(Add);
                    DB.SaveChanges();
                }
                else
                    return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        #region Удаление из корзины

        [Authorize]
        public ActionResult RemoveGameFromBasket(int Id)
        {
            try
            {
                using (DBContext DB = new DBContext())
                {
                    Game Removing = DB.Games.FirstOrDefault(n => n.Id == Id);
                    User Current = DB.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
                    DB.Entry(Current).Collection(u => u.UserGames).Load();

                    if (Current.UserGames.FirstOrDefault(x => x.Id == Removing.Id) != null)
                    {
                        if (Removing != null)
                        {
                            return View(Removing);
                        }
                        else
                            return HttpNotFound();
                    }
                    else
                        return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public ActionResult RemoveGameFromBasket(Game game)
        {
            using (DBContext DB = new DBContext())
            {
                Game removing = DB.Games.FirstOrDefault(n => n.Id == game.Id);
                if (removing != null)
                {
                    DB.Users.FirstOrDefault(x => x.Login == User.Identity.Name).UserGames.Remove(game);
                    DB.SaveChanges();
                }
                else
                    return HttpNotFound();
            }

            return RedirectToAction("Index");
        }
        #endregion

        [Authorize(Roles = "Admin")]
        public ActionResult AddGame()
        {
            using (DBContext DB = new DBContext())
            {
                ViewBag.Categories = DB.Categories.ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddGame(string Name, string Description, int selectedCateg, int Price)
        {
            using (DBContext DB = new DBContext())
            {
                DB.Games.Add(new Game { Name = Name, CategoryId = selectedCateg, Description = Description, Price = Price });
                DB.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}