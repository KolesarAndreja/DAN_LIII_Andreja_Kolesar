using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLIX.Service
{
    class Service
    {

        #region GET ROLE
        public static string GetRole(int id)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblStaff res = (from x in context.tblStaffs where x.userId == id select x).FirstOrDefault();

                    if (res != null)
                    {
                        return "staff";
                    }
                    else 
                    {
                        return "manager";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region FIND USER BY USERNAME AND PASSWORD

        public static tblUser IsValidUser(string username, string password)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblUser res = (from x in context.tblUsers where x.username==username && x.password == password select x).FirstOrDefault();
                    return res;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }


        public static tblManager getManager(int userId)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblManager res = (from x in context.tblManagers where x.userId == userId select x).FirstOrDefault();
                    return res;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }



        public static tblStaff getStaff(int userId)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblStaff res = (from x in context.tblStaffs where x.userId == userId select x).FirstOrDefault();
                    return res;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }

        public static tblStaff getStaff(vwStaff s)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblUser user = (from x in context.tblUsers where x.username == s.username select x).FirstOrDefault();
                    tblStaff res = (from x in context.tblStaffs where x.userId == user.userId select x).FirstOrDefault();
                    return res;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }
        #endregion




        #region get qualification
        public static string getGenderString(int? id)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblGender res = (from x in context.tblGenders where x.id == id select x).FirstOrDefault();
                    return res.name;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }

        public static int getQualificationNumber(int? id)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblProfessionalQualification res = (from x in context.tblProfessionalQualifications where x.id == id select x).FirstOrDefault();
                    int q;
                    switch (res.name)
                    {
                        case "I":
                            q = 1;
                            break;
                        case "II":
                            q = 2;
                            break;
                        case "III":
                            q = 3;
                            break;
                        case "IV":
                            q = 4;
                            break;
                        case "V":
                            q = 5;
                            break;
                        case "VI":
                            q = 6;
                            break;
                        case "VII":
                            q = 7;
                            break;
                        default:
                            q = 0;
                            break;
                    }
                    return q;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return 0;
            }
        }

        public static string getEngagmentName(int? id)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    tblEngagement res = (from x in context.tblEngagements where x.id == id select x).FirstOrDefault();
                    return res.name;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }

        #endregion

        #region ADD USER
        public static tblUser AddUser(tblUser user)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    if (user.userId == 0)
                    {
                        //add 
                        tblUser newUser = new tblUser();
                        newUser.username = user.username;
                        newUser.password = user.password;
                        newUser.dateOfBirth = user.dateOfBirth;
                        newUser.email = user.email;
                        newUser.fullname = user.fullname;
                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.userId = newUser.userId;
                        return user;
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region ADD MANAGER
        public static tblManager AddManager(tblManager manager)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    if (manager.managerId == 0)
                    {
                        //add 
                        tblManager newManager = new tblManager();
                        newManager.name = "manager";
                        newManager.userId = manager.userId;
                        newManager.floorNumber = manager.floorNumber;
                        newManager.workExperience = manager.workExperience;
                        newManager.qualificationId = manager.qualificationId;
                        context.tblManagers.Add(newManager);
                        context.SaveChanges();
                        manager.managerId = newManager.managerId;
                        return manager;
                    }
                    return manager;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region ADD STAFF
        public static tblStaff AddStaff(tblStaff staff)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    if (staff.staffId == 0)
                    {
                        //add 
                        tblStaff newStaff = new tblStaff();
                        newStaff.userId = staff.userId;
                        newStaff.citizenship = staff.citizenship;
                        newStaff.floorNumber = staff.floorNumber;
                        newStaff.engegamentId = staff.engegamentId;
                        newStaff.genderId = staff.genderId;
                        context.tblStaffs.Add(newStaff);
                        context.SaveChanges();
                        staff.staffId = newStaff.staffId;
                        return staff;
                    }
                    else
                    {

                        tblStaff staffToEdit = (from x in context.tblStaffs where x.staffId == staff.staffId select x).FirstOrDefault();
                        staffToEdit.salary = staff.salary;
                        context.SaveChanges();
                        return staff;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region GET LISTS

        public static List<tblGender> GetGenderList()
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    List<tblGender> list = new List<tblGender>();
                    list = (from x in context.tblGenders select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public static List<tblProfessionalQualification> GetQualificationList()
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    List<tblProfessionalQualification> list = new List<tblProfessionalQualification>();
                    list = (from x in context.tblProfessionalQualifications select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }


        public static List<tblEngagement> GetEngagementList()
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    List<tblEngagement> list = new List<tblEngagement>();
                    list = (from x in context.tblEngagements select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }


        public static List<vwStaff> GetFloorEmployees(int n)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    List<vwStaff> list = new List<vwStaff>();
                    list = (from x in context.vwStaffs  where x.floorNumber == n select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region VALIDATION

        public static bool UsedUsername(string name)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    bool b = (from x in context.tblUsers where x.username == name select x).Any();
                    return b;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return true;
            }
        }

        public static bool SupervisedFloor(int n)
        {
            try
            {
                using (dbHotelEntities context = new dbHotelEntities())
                {
                    bool b = (from x in context.tblManagers where x.floorNumber == n select x).Any();
                    return b;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }
        #endregion

    }
}
