using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/vuelo")]
    public class VueloController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Vuelo vuelo = new Vuelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT VUE_ID, AV_ID, VUE_ORIGEN, VUE_DESTINO, VUE_CANT_PASAJEROS FROM VUELO
                                                            WHERE VUE_ID = @VUE_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@VUE_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        vuelo.VUE_ID = sqlDataReader.GetInt32(0);
                        vuelo.AV_ID = sqlDataReader.GetInt32(1);
                        vuelo.VUE_ORIGEN = sqlDataReader.GetString(2);
                        vuelo.VUE_DESTINO = sqlDataReader.GetString(3);
                        vuelo.VUE_CANT_PASAJEROS = sqlDataReader.GetInt32(4);
                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(vuelo);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Vuelo> vuelos = new List<Vuelo>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT VUE_ID, AV_ID, VUE_ORIGEN, VUE_DESTINO, VUE_CANT_PASAJEROS FROM VUELO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Vuelo vuelo = new Vuelo()
                        {
                            VUE_ID = sqlDataReader.GetInt32(0),
                            AV_ID = sqlDataReader.GetInt32(1),
                            VUE_ORIGEN = sqlDataReader.GetString(2),
                            VUE_DESTINO = sqlDataReader.GetString(3),
                            VUE_CANT_PASAJEROS = sqlDataReader.GetInt32(4)
                        };

                        vuelos.Add(vuelo);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(vuelos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Vuelo vuelo)
        {
            if (vuelo == null)
                return BadRequest();

            if (RegistrarVuelo(vuelo))
                return Ok(vuelo);
            else
                return InternalServerError();
        }

        private bool RegistrarVuelo(Vuelo vuelo)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO VUELO (AV_ID, VUE_ORIGEN, VUE_DESTINO, VUE_CANT_PASAJEROS) 
                                                            VALUES (@AV_ID, @VUE_ORIGEN, @VUE_DESTINO, @VUE_CANT_PASAJEROS)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@VUE_ID", vuelo.VUE_ID);
                sqlCommand.Parameters.AddWithValue("@AV_ID", vuelo.AV_ID);
                sqlCommand.Parameters.AddWithValue("@VUE_ORIGEN", vuelo.VUE_ORIGEN);
                sqlCommand.Parameters.AddWithValue("@VUE_DESTINO", vuelo.VUE_DESTINO);
                sqlCommand.Parameters.AddWithValue("@VUE_CANT_PASAJEROS", vuelo.VUE_CANT_PASAJEROS);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }

        [HttpPut]
        public IHttpActionResult Actualizar(Vuelo vuelo)
        {
            if (vuelo == null)
                return BadRequest();

            if (ActualizarVuelo(vuelo))
                return Ok(vuelo);
            else
                return InternalServerError();
        }

        private bool ActualizarVuelo(Vuelo vuelo)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE VUELO SET AV_ID = @AV_ID, 
                                                                          VUE_ORIGEN = @VUE_ORIGEN, 
                                                                          VUE_DESTINO = @VUE_DESTINO, 
                                                                          VUE_CANT_PASAJEROS = @VUE_CANT_PASAJEROS
                                                                      WHERE VUE_ID = @VUE_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@VUE_ID", vuelo.VUE_ID);
                sqlCommand.Parameters.AddWithValue("@AV_ID", vuelo.AV_ID);
                sqlCommand.Parameters.AddWithValue("@VUE_ORIGEN", vuelo.VUE_ORIGEN);
                sqlCommand.Parameters.AddWithValue("@VUE_DESTINO", vuelo.VUE_DESTINO);
                sqlCommand.Parameters.AddWithValue("@VUE_CANT_PASAJEROS", vuelo.VUE_CANT_PASAJEROS);

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

            if (EliminarVuelo(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarVuelo(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE VUELO
                                                         WHERE VUE_ID = @VUE_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@VUE_ID", id);

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