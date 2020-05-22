/**
 * @(#) ISUser.cs
 */
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
namespace WebApplication6.Models
{
	public class ISUser
	{
		public string nickname;
		
		ISUser users_friend;
		
		ISUser user;
		
		//PersonalMessage sentMessage;
		
		//PersonalMessage receivedMessage;
		
		string password;
		
		DateTime createDate;
		
		string description;

		DateTime lastLogin;
		
		//ForumPost created_forum_post;
		
		//Reply created_reply;
		
		public void getUserInfo(  )
		{
			
		}
		
		public void registerUser(  )
		{
			
		}
		
		public void updateUserInfo(  )
		{
			
		}
		
		public void getFriends(  )
		{
			
		}
		
		public void addFriend(  )
		{
			
		}
		
		public void removeFriend(  )
		{
			
		}
		
	}
	
}
