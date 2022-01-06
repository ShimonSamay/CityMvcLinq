using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CityMvcLinq.Models;

namespace CityMvcLinq.Controllers.api
{
    public class ResidentController : ApiController
    {
       
        static string connecectionString = "Data Source=SHIMONSAMAY;Initial Catalog=CityDB;Integrated Security=True;Pooling=False";
        ResidentDataContext CityDB = new ResidentDataContext(connecectionString);
        public IHttpActionResult Get()
        {
           try
            {
                return Ok(CityDB.Residents.ToList());
            }
            catch(SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);  
            }
            catch (Exception ex)
            {
                return BadRequest (ex.Message);
            }
            
        }

     
        public IHttpActionResult Get(int id)
        {
            try {
               Resident someResident = CityDB.Residents.First(resident => resident.Id == id);
               return Ok(new { someResident });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

   
        public IHttpActionResult Post([FromBody]Resident value)
        {
            try {
                CityDB.Residents.InsertOnSubmit(value);
                CityDB.SubmitChanges();
                return Ok(CityDB.Residents.ToList());
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

      
        public IHttpActionResult Put(int id, [FromBody] Resident value)
        {
            try
            {
                Resident someResident = CityDB.Residents.First (resident => resident.Id == id);
                someResident.FirstName = value.FirstName;
                someResident.LastName = value.LastName;
                someResident.BirthDate = value.BirthDate;
                someResident.YearsLiving = value.YearsLiving;
                someResident.Addres = value.Addres;
                CityDB.SubmitChanges ();
                return Ok(CityDB.Residents.ToList());
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }

        }

       
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Resident someResident = CityDB.Residents.First(resident => resident.Id == id);
                CityDB.Residents.DeleteOnSubmit(someResident);
                CityDB.SubmitChanges();
                return Ok(CityDB.Residents.ToList());
            }
            catch(SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }


    }
}
