using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLIX.Model
{
    class Person
    {
        public int id { get; set; }
        private string path = @"..\..\OwnerAccess.txt";
        public string username { get; set; }
        public string password { get; set; }
        public string role
        {
            get
            {
                if (id != 0)
                {
                    return Service.Service.GetRole(id);
                }
                else
                {
                    return null;
                }
                
            }
        }


        public bool isOwner()
        {
            string[] lines = File.ReadAllLines(path);
            if (lines[0].Split(':')[1] == username && lines[1].Split(':')[1] == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
