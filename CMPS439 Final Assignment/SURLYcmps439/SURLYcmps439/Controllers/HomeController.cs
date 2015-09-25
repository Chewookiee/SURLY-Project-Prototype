using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SURLYcmps439_DAL;

namespace SURLYcmps439.Controllers
{
    public class HomeController : Controller
    {
        ResolveQuery resolveQuery = new ResolveQuery();

        public ActionResult Index()
        {
            try
            {
                resolveQuery.Load(
                    "RELATION USER      (ID         NUM     11 " +
                                        "USERNAME   CHAR    40 " +
                                        "F_NAME     CHAR    40 " +
                                        "L_NAME     CHAR    40); " + 
                    "RELATION CONTACT   (USER_ID    NUM     11 " +
                                        "PHONE      CHAR    13 " +
                                        "EMAIL      CHAR    90);" +

                    "INPUT USER 1 Chawookiee Matthew Puneky;" +
                                "2 d00ditsLee Lee White;" +
                                "3 aGross   Aaron   Gross;" +
                                "4 Pinto   KeKe   Bear;*" +
                            "CONTACT 1 9857730086 'matthew.puneky@gmail.com';" +
                                    "2 5045550000 'lee.white@gmail.com';" +
                                    "3 8004212322 'aaron.gross@gmail.com';" +
                                    "1 9999999999 'fuckYou@what.bitch';*" +
                    "END_INPUT;"

                    /*
                    "RELATION COURSE 	(CNUM 	CHAR 	 8 "+
	                                     "TITLE 	CHAR 	30 " +
	                                     "CREDITS 	NUM 	 4); " +
                    "RELATION PREREQ 	(CNUM1 	CHAR 	 8 " +
	                     "CNUM2 	CHAR 	 8); " +
                    "RELATION OFFERING 	(CNUM 	CHAR 	 8 " +
	                     "SECTION 	NUM 	 5 " +
                         "START_HOUR  CHAR 	 5 " +
	                     "END_HOUR 	CHAR 	 5 " +
	                     "DAYS 	CHAR 	 5 " +
	                     "ROOM 	CHAR 	10 " +
	                     "INSTRUCTOR CHAR 	20); " +
                    "RELATION STAFF 	(NAME 	CHAR 	20, " +
	                     "SPOUSE 	CHAR 	10, " +
	                     "RANK 	CHAR 	 5, " +
	                     "CAMPUSADDR CHAR 	10, " +
	                     "EXTENSION 	CHAR 	 9); " +
                    "RELATION INTERESTS 	(NAME 	CHAR 	20 " +
	                     "INTEREST 	CHAR 	30); " +
                    "RELATION DEPT 	(NAME 	CHAR 	20 " +
	                     "DEPT 	CHAR 	 4);" +
                    "INSERT OFFERING CS1410 27935 8:55 9:45 MWF H120 HAMPTON;" +
                    "INPUT COURSE CS141O 'BUSINESS FORTRAN' 3;" +
                            "CS341O COBOL              3;*" +
                        "INTERESTS THOMPSON DBMS;" +
                        "PAO   PROJ ;" +
                        "THOMPSON   AI ;" +
                        "DENEKE   AI ;" +
                        "PUNEKY   AWESOME ;" +
                        "PUNEKY   BEST ;" +
                        "ALKADI   PROJ ;" +
                        "DENEKE   DBMS ;" +
                        "KOLATA   AI ;" +
                        "MG   SYS ;" +
                        "ALKADI   SYS;*" +
                    "END_INPUT;"
                     * */
                );
            }
            catch(Exception e)
            {
                string temp = e.Message;
            }

            
            try
            {
                resolveQuery.Load(
                    "JOIN (USER AND CONTACT) OVER (ID AND USER_ID);"
                );
            }
            catch(Exception e)
            {
                string temp = e.Message;
            }
            
            /*
            try
            {
                resolveQuery.Load(
                    "SELECT (INTERESTS) " +
                        "WHERE (INTEREST = AI OR " +
                                "NAME = ALKADI AND " +
                                "INTEREST = SYS OR " +
                                "NAME = ALKADI OR " +
                                "NAME = PUNEKY AND " +
                                "INTEREST = BEST);"
                );

            }
            catch (Exception e)
            {
                string temp = e.Message;
            }
            */

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}