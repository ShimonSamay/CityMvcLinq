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
    public class SchoolController : ApiController
    {
        
        static string connectionString = "Data Source=SHIMONSAMAY;Initial Catalog=CityDB;Integrated Security=True;Pooling=False";
        SchoolContextDataContext schoolDB = new SchoolContextDataContext(connectionString);
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(schoolDB.Schools.ToList());
            }
            catch (SqlException x)
            {
                return BadRequest(x.Message);   
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }

        
        public IHttpActionResult Get(int id)
        {
            try
            {
                School someSchool = schoolDB.Schools.First(school => school.Id == id);
                return Ok(new { someSchool });
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        public IHttpActionResult Post([FromBody]School value)
        {
            try
            {
                schoolDB.Schools.InsertOnSubmit(value);
                schoolDB.SubmitChanges();
                return Ok(schoolDB.Schools.ToList());
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        public IHttpActionResult Put(int id, [FromBody] School value)
        {try
            {
                School school = schoolDB.Schools.First(anySchool => anySchool.Id == id);
                school.Addres = value.Addres;
                school.SchoolName = value.SchoolName;
                school.IfPublic = value.IfPublic;
                school.Stusents = value.Stusents;
                return Ok(schoolDB.Schools.ToList());
            }
            catch(SqlException x)
            {
                return BadRequest(x.Message);
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
                School foundSchool = schoolDB.Schools.First(school => school.Id == id);
                schoolDB.Schools.DeleteOnSubmit(foundSchool);
                schoolDB.SubmitChanges();
                return Ok(schoolDB.Schools.ToList());
            }
            catch(SqlException x)
            {
                return BadRequest(x.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }
    }
}
