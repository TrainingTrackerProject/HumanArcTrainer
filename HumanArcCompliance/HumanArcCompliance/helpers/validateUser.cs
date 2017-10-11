using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using HumanArcCompliance.Models;

namespace HumanArcCompliance
{
    public class validateUser
    {
        public string validate(){
            try
            {
                string path = "LDAP://xxxx/CN=Users,DC=firm,DC=xxxx,DC=com";
                string filter = "(&(objectCategory=person)(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2))";
                string[] propertiesToLoad = new string[1] { "name" };
                using (DirectoryEntry root = new DirectoryEntry(path, "xx\\xxxx", "xxxx"))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(root, filter, propertiesToLoad))
                    {
                        using (SearchResultCollection results = searcher.FindAll())
                        {
                            foreach (SearchResult result in results)
                            {
                                string name = (string)result.Properties["name"][0];
                            }
                        }
                    }
                    
                }
                
            }
            catch
            {
            }
            return "";
        }

        
    }
}