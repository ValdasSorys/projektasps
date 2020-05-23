using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication6
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdministrationNewsCreate",
                url: "admin/news/create",
                defaults: new { controller = "AdministrationNews", action = "openAdministrationNewsCreate" }
            );

            routes.MapRoute(
                name: "AdministrationNewsEdit",
                url: "admin/news/edit/{id}",
                defaults: new { controller = "AdministrationNews", action = "openAdministrationNewsEdit" }
            );

            routes.MapRoute(
                name: "AdministrationNewsDelete",
                url: "admin/news/delete/{id}",
                defaults: new { controller = "AdministrationNews", action = "deleteNewsArticle" }
            );

            routes.MapRoute(
                name: "AdministrationNewsList",
                url: "admin/news",
                defaults: new { controller = "AdministrationNews", action = "openAdministrationNewsList" }
            );

            routes.MapRoute(
                name: "AdministrationMain",
                url: "admin",
                defaults: new { controller = "Administration", action = "openAdministrationMain" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "TestLogin", action = "Index" }
            );

            routes.MapRoute(
                name: "LoginUser",
                url: "loginUser",
                defaults: new { controller = "TestLogin", action = "LoginUser" }
            );

            routes.MapRoute(
                name: "TournamentList",
                url: "tournaments",
                defaults: new { controller = "TournamentList", action = "openTournamentList" }
                );

            routes.MapRoute(
                name: "TournamentParticipate",
                url: "tournament/participate/{id}",
                defaults: new { controller = "Tournaments", action = "RegisterTournament" }
                );

            routes.MapRoute(
                name: "TournamentInfo",
                url: "tournament/{id}",
                defaults: new { controller = "TournamentList", action = "openTournamentInfo" }
            );

            routes.MapRoute(
               name: "TournamentMain",
               url: "tournamentsmain",
               defaults: new { controller = "TournamentList", action = "openTournamentPage" }
               );

            routes.MapRoute(
               name: "TournamentPlay",
               url: "tournamentplay",
               defaults: new { controller = "Tournaments", action = "openTournamentPage" }
               );

            routes.MapRoute(
               name: "TournamentActive",
               url: "tournamentactive",
               defaults: new { controller = "Tournaments", action = "openActiveTournaments" }
               );

            routes.MapRoute(
               name: "NewsList",
               url: "news",
               defaults: new { controller = "News", action = "openNewsList" }
               );

            routes.MapRoute(
               name: "TeamList",
               url: "teams",
               defaults: new { controller = "TeamList", action = "openTeamList" }
               );

            routes.MapRoute(
               name: "TeamCreate",
               url: "team/create",
               defaults: new { controller = "CreateTeam", action = "openCreateTeamView" }
               );

            routes.MapRoute(
               name: "TeamJoin",
               url: "team/join/{id}",
               defaults: new { controller = "PlayerRequest", action = "createPlayerJoinRequest" }
               );

            routes.MapRoute(
               name: "TeamDetails",
               url: "team/{id}",
               defaults: new { controller = "TeamInfo", action = "openTeamInfo" }
               );


            routes.MapRoute(
               name: "openMatchView",
               url: "queue",
               defaults: new { controller = "MatchPlay", action = "openMatchView" }
               );
            routes.MapRoute(
               name: "getPlayersOngoingMatch",
               url: "getMatch",
               defaults: new { controller = "MatchPlay", action = "getPlayersOngoingMatch", id = UrlParameter.Optional}
               );

            routes.MapRoute(
               name: "removeFromQueue",
               url: "removeFromQueue",
               defaults: new { controller = "MatchPlay", action = "removeFromQueue", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "getMessagesString",
               url: "getMessagesString",
               defaults: new { controller = "MatchPlay", action = "getMessagesString", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "writeMessage",
               url: "writeMessage",
               defaults: new { controller = "MatchPlay", action = "writeMessage", matchid_ = UrlParameter.Optional, playerid_ = UrlParameter.Optional, text = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "admitDefeat",
               url: "admitDefeat",
               defaults: new { controller = "MatchPlay", action = "admitDefeat", matchid_ = UrlParameter.Optional, playerid_ = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "checkMatchConcluded",
               url: "checkMatchConcluded",
               defaults: new { controller = "MatchPlay", action = "checkMatchConcluded", id = UrlParameter.Optional }
               );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}
