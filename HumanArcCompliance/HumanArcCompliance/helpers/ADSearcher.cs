﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using HumanArcCompliance.Models;
 
namespace HumanArcCompliance.helpers
{
    public class ADSearcher
    {
        //User and Password for connecting to local machine -- Will be stored in config files
        String ADUser_Id = "SCLDC\\Administrator"; //make sure user name has domain name.
        String Password = "Sheen5454!";
        //connection to active directory 
        PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

        //PrincipalContext ctx = new PrincipalContext(
        //                                 ContextType.Domain,
        //                                 "SCLDC.com",
        //                                 "CN=Users,DC=SCLDC,DC=com",
        //                                 "administrator",
        //                                 "Sheen5454!");
        private DirectorySearcher ds = new DirectorySearcher();
        private SortOption option = new System.DirectoryServices.SortOption("sn", System.DirectoryServices.SortDirection.Ascending);


        /// <summary>
        /// find Current Username finds the current username based on the Request.ServerVariables["AUTH_USER"] member.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>UserPrincipal for obtaining different information about the user account</returns>
        public UserPrincipal findCurrentUserName(HttpRequestBase req)
        {
            string username = req.LogonUserIdentity.Name;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            UserPrincipal up = UserPrincipal.FindByIdentity(ctx, username);
            return up;
        }

        /// <summary>
        /// Finds the available AD user information based off of the userPrincipal
        /// </summary>
        /// <param name="req"></param>
        /// <returns>UserPrincipal for obtaining different information about the current user/searched account</returns>
        public ADUser findByUserName(UserPrincipal currentUser)
        {
            // find currently logged in user LDAP Query
            ds.Filter = ("(&(objectClass=user)(sAMAccountName=" + currentUser.SamAccountName + "))");
            ds.Sort = option;
            ADUser myADUser = new ADUser();
            try
            {
                SearchResult adSearchResult = ds.FindOne();
                DirectoryEntry de = adSearchResult.GetDirectoryEntry();
                // Goes through all properties
                foreach (string Key in de.Properties.PropertyNames)
                {
                    myADUser = checkFields(de, Key, myADUser);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return myADUser;
        }

        //If property matches with a specific property name then give ADUser that value
        public ADUser checkFields(DirectoryEntry de, String Key, ADUser item)
        {
            //property names will be declared in config files
            if (Key == "givenName")
            {
                item.givenName = de.Properties["givenName"].Value.ToString();
            }
            if (Key == "sn")
            {
                item.sn = de.Properties["sn"].Value.ToString();
            }
            if (Key == "mail")
            {
                item.mail = de.Properties["mail"].Value.ToString();
            }
            return item;
        }

        //public void setSessionVars(ADUser user)
        //{
        //    HttpContext.Current.Session["currentUser"] = user;
        //}

    }
}