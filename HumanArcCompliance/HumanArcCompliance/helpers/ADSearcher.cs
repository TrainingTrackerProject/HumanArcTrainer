using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using HumanArcCompliance.Models;
using System.Text.RegularExpressions;

namespace HumanArcCompliance.helpers
{
    public class ADSearcher
    {
        //User and Password for connecting to local machine -- Will be stored in config files
        //String ADUser_Id = ConfigurationManager.AppSettings["domain"] + "\\" + ConfigurationManager.AppSettings["superUserName"]; //make sure user name has domain name.
        //String Password = ConfigurationManager.AppSettings["superUserPass"];
        //connection to active directory 

        
        PrincipalContext ctx = new PrincipalContext(ContextType.Domain);


        private DirectorySearcher ds = new DirectorySearcher();
        private SortOption option = new System.DirectoryServices.SortOption("sn", System.DirectoryServices.SortDirection.Ascending);



        /// <summary>
        /// find Current Username finds the current username based on the Request.ServerVariables["AUTH_USER"] member.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>UserPrincipal for obtaining different information about the user account</returns>
        public UserPrincipal findCurrentUserName(HttpRequestBase req)
        {
            //string username = "jsmith"; 
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
            //ds.Filter = ("(&(objectClass=user)(sAMAccountName = "+currentUser.SamAccountName+"))"); //+ currentUser.SamAccountName + "))");
            ds.Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + currentUser.SamAccountName + "))";
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
            if (Key == ConfigurationManager.AppSettings["givenName"])
            {
                item.givenName = de.Properties[ConfigurationManager.AppSettings["givenName"]].Value.ToString();
            }
            if (Key == ConfigurationManager.AppSettings["surname"])
            {
                item.sn = de.Properties[ConfigurationManager.AppSettings["surname"]].Value.ToString();
            }
            if (Key == ConfigurationManager.AppSettings["email"])
            {
                item.mail = de.Properties[ConfigurationManager.AppSettings["email"]].Value.ToString();
            }
            if (Key == ConfigurationManager.AppSettings["sAMAccountName"])
            {
                item.sAMAccountName = de.Properties[ConfigurationManager.AppSettings["sAMAccountName"]].Value.ToString();
            }
            if (Key == ConfigurationManager.AppSettings["memberOf"])
            {

                List<String> memberGroups = new List<String>();
                foreach (String s in de.Properties[ConfigurationManager.AppSettings["memberOf"]])
                {
                    List<string> cn = parseMemberGroup(s);
                    foreach(string str in cn)
                    {
                        memberGroups.Add(str);
                    }                   
                }
                item.memberOf = memberGroups.ToArray();


                //item.memberOf = memberString.Split(',').Split(',')[0];
            }
            return item;
        }

        public List<string> parseMemberGroup(String memberGroup)
        {
            List<string> cn = new List<string>();
            String[] parsedGroup = memberGroup.Split(',');
            for (int i = 0; i < parsedGroup.Length; i++)
            {
                if (parsedGroup[i].Substring(0, 2).Equals("CN"))
                {
                    String[] cnParsed = parsedGroup[i].Split('=');
                    cn.Add(cnParsed[1]);
                }
            }
            return cn;
        }



    }
}