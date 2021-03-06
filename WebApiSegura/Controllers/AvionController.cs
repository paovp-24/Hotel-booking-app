using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    //[Authorize]
    [RoutePrefix("api/avion")]
    public class AvionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Avion avion = new Avion();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AV_ID, AERO_ID, AV_CAPACIDAD_TOTAL, AV_MARCA, AV_TIPO_AVION, AV_MODELO
                                                            FROM     AVION
                                                            WHERE AV_ID = @AV_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AV_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        avion.AV_ID = sqlDataReader.GetInt32(0);
                        avion.AERO_ID = sqlDataReader.GetInt32(1);
                        avion.AV_CAPACIDAD_TOTAL = sqlDataReader.GetInt32(2);
                        avion.AV_MARCA = sqlDataReader.GetString(3);
                        avion.AV_TIPO_AVION = sqlDataReader.GetString(4);
                        avion.AV_MODELO = sqlDataReader.GetString(5);


                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(avion);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Avion> aviones = new List<Avion>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT AV_ID, AERO_ID, AV_CAPACIDAD_TOTAL, AV_MARCA, AV_TIPO_AVION, AV_MODELO
                                                            FROM     AVION", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Avion avion = new Avion()
                        {
                            AV_ID = sqlDataReader.GetInt32(0),
                            AERO_ID = sqlDataReader.GetInt32(1),
                            AV_CAPACIDAD_TOTAL = sqlDataReader.GetInt32(2),
                            AV_MARCA = sqlDataReader.GetString(3),
                            AV_TIPO_AVION = sqlDataReader.GetString(4),
                            AV_MODELO = sqlDataReader.GetString(5)
                        };

                        aviones.Add(avion);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(aviones);
        }





        [HttpPost]
        public IHttpActionResult Ingresar(Avion avion)
        {
            if (avion == null)
                return BadRequest();

            if (RegistrarAvion(avion))
                return Ok(avion);
            else
                return InternalServerError();
        }

        private bool RegistrarAvion(Avion avion)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO AVION (AERO_ID, AV_CAPACIDAD_TOTAL, AV_MARCA, 
                                                        AV_TIPO_AVION, AV_MODELO) VALUES (@AERO_ID, @AV_CAPACIDAD_TOTAL,
                                                        @AV_MARCA, @AV_TIPO_AVION, @AV_MODELO)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@AERO_ID", avion.AERO_ID);
                sqlCommand.Parameters.AddWithValue("@AV_CAPACIDAD_TOTAL", avion.AV_CAPACIDAD_TOTAL);
                sqlCommand.Parameters.AddWithValue("@AV_MARCA", avion.AV_MARCA);
                sqlCommand.Parameters.AddWithValue("@AV_TIPO_AVION", avion.AV_TIPO_AVION);
                sqlCommand.Parameters.AddWithValue("@AV_MODELO", avion.AV_MODELO);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }







        [HttpPut]
        public IHttpActionResult Actualizar(Avion avion)
        {
            if (avion == null)
                return BadRequest();

            if (ActualizarAvion(avion))
                return Ok(avion);
            else
                return InternalServerError();
        }

        private bool ActualizarAvion(Avion avion)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE AVION SET AERO_ID = @AERO_ID,
                                                                        AV_CAPACIDAD_TOTAL = @AV_CAPACIDAD_TOTAL, 
                                                                        AV_MARCA = @AV_MARCA, 
                                                                        AV_TIPO_AVION = @AV_TIPO_AVION, 
                                                                        AV_MODELO = @AV_MODELO
                                                                      
                                                                    WHERE AV_ID = @AV_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@AV_ID", avion.AV_ID);
                sqlCommand.Parameters.AddWithValue("@AERO_ID", avion.AERO_ID);
                sqlCommand.Parameters.AddWithValue("@AV_CAPACIDAD_TOTAL", avion.AV_CAPACIDAD_TOTAL);
                sqlCommand.Parameters.AddWithValue("@AV_MARCA", avion.AV_MARCA);
                sqlCommand.Parameters.AddWithValue("@AV_TIPO_AVION", avion.AV_TIPO_AVION);
                sqlCommand.Parameters.AddWithValue("@AV_MODELO", avion.AV_MODELO);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }




        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            if (EliminarAvion(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarAvion(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE AVION
                                                       WHERE AV_ID = @AV_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@AV_ID", id);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }







    }
}