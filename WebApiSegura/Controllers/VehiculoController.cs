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
    [Authorize]
    [RoutePrefix("api/vehiculo")]
    public class VehiculoController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {

            Vehiculo vehiculo = new Vehiculo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT VEH_ID, SUC_ID, VEH_PLACA, VEH_MARCA, VEH_MODELO, 
                                                            VEH_ESTADO, VEH_TIPO, VEH_TRACCION, VEH_CANT_PASAJEROS, VEH_TRANSMISION
                                                            FROM  VEHICULO                                                           
                                                            WHERE VEH_ID = @VEH_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@VEH_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        vehiculo.VEH_ID = sqlDataReader.GetInt32(0);
                        vehiculo.SUC_ID = sqlDataReader.GetInt32(1);
                        vehiculo.VEH_PLACA = sqlDataReader.GetString(2);
                        vehiculo.VEH_MARCA = sqlDataReader.GetString(3);
                        vehiculo.VEH_MODELO = sqlDataReader.GetString(4);
                        vehiculo.VEH_ESTADO = sqlDataReader.GetString(5);
                        vehiculo.VEH_TIPO = sqlDataReader.GetString(6);
                        vehiculo.VEH_TRACCION = sqlDataReader.GetString(7);
                        vehiculo.VEH_CANT_PASAJEROS = sqlDataReader.GetInt32(8);
                        vehiculo.VEH_TRANSMISION = sqlDataReader.GetString(9);


                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(vehiculo);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT VEH_ID, SUC_ID, VEH_PLACA, VEH_MARCA, VEH_MODELO, 
                                                            VEH_ESTADO, VEH_TIPO, VEH_TRACCION, VEH_CANT_PASAJEROS, VEH_TRANSMISION
                                                            FROM  VEHICULO ", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Vehiculo vehiculo = new Vehiculo()
                        {
                            VEH_ID = sqlDataReader.GetInt32(0),
                            SUC_ID = sqlDataReader.GetInt32(1),
                            VEH_PLACA = sqlDataReader.GetString(2),
                            VEH_MARCA = sqlDataReader.GetString(3),
                            VEH_MODELO = sqlDataReader.GetString(4),
                            VEH_ESTADO = sqlDataReader.GetString(5),
                            VEH_TIPO = sqlDataReader.GetString(6),
                            VEH_TRACCION = sqlDataReader.GetString(7),
                            VEH_CANT_PASAJEROS = sqlDataReader.GetInt32(8),
                            VEH_TRANSMISION = sqlDataReader.GetString(9),
                        };

                        vehiculos.Add(vehiculo);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(vehiculos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Vehiculo vehiculo)
        {
            if (vehiculo == null)
                return BadRequest();

            if (RegistrarVehiculo(vehiculo))
                return Ok(vehiculo);
            else
                return InternalServerError();
        }

        private bool RegistrarVehiculo(Vehiculo vehiculo)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO VEHICULO(SUC_ID,VEH_PLACA,VEH_MARCA,VEH_MODELO,
                                                        VEH_ESTADO,VEH_TIPO,VEH_TRACCION,VEH_CANT_PASAJEROS,VEH_TRANSMISION)
                                                        VALUES(@SUC_ID,@VEH_PLACA,@VEH_MARCA,@VEH_MODELO,@VEH_ESTADO,@VEH_TIPO
                                                        ,@VEH_TRACCION,@VEH_CANT_PASAJEROS,@VEH_TRANSMISION)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@SUC_ID", vehiculo.SUC_ID);
                sqlCommand.Parameters.AddWithValue("@VEH_PLACA", vehiculo.VEH_PLACA);
                sqlCommand.Parameters.AddWithValue("@VEH_MARCA", vehiculo.VEH_MARCA);
                sqlCommand.Parameters.AddWithValue("@VEH_MODELO", vehiculo.VEH_MODELO);
                sqlCommand.Parameters.AddWithValue("@VEH_ESTADO", vehiculo.VEH_ESTADO);
                sqlCommand.Parameters.AddWithValue("@VEH_TIPO", vehiculo.VEH_TIPO);
                sqlCommand.Parameters.AddWithValue("@VEH_TRACCION", vehiculo.VEH_TRACCION);
                sqlCommand.Parameters.AddWithValue("@VEH_CANT_PASAJEROS", vehiculo.VEH_CANT_PASAJEROS);
                sqlCommand.Parameters.AddWithValue("@VEH_TRANSMISION", vehiculo.VEH_TRANSMISION);


                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }

        [HttpPut]
        public IHttpActionResult Actualizar(Vehiculo vehiculo)
        {
            if (vehiculo == null)
                return BadRequest();

            if (ActualizarVehiculo(vehiculo))
                return Ok(vehiculo);
            else
                return InternalServerError();
        }

        private bool ActualizarVehiculo(Vehiculo vehiculo)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE VEHICULO SET SUC_ID = @SUC_ID,
                                                                             VEH_PLACA = @VEH_PLACA,
                                                                             VEH_MARCA = @VEH_MARCA,
                                                                             VEH_MODELO = @VEH_MODELO,
                                                                             VEH_ESTADO = @VEH_ESTADO,
                                                                             VEH_TIPO = @VEH_TIPO,
                                                                             VEH_TRACCION = @VEH_TRACCION,
                                                                             VEH_CANT_PASAJEROS = @VEH_CANT_PASAJEROS,
                                                                             VEH_TRANSMISION = @VEH_TRANSMISION
                                                                             WHERE VEH_ID = @VEH_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@VEH_ID", vehiculo.VEH_ID);
                sqlCommand.Parameters.AddWithValue("@SUC_ID", vehiculo.SUC_ID);
                sqlCommand.Parameters.AddWithValue("@VEH_PLACA", vehiculo.VEH_PLACA);
                sqlCommand.Parameters.AddWithValue("@VEH_MARCA", vehiculo.VEH_MARCA);
                sqlCommand.Parameters.AddWithValue("@VEH_MODELO", vehiculo.VEH_MODELO);
                sqlCommand.Parameters.AddWithValue("@VEH_ESTADO", vehiculo.VEH_ESTADO);
                sqlCommand.Parameters.AddWithValue("@VEH_TIPO", vehiculo.VEH_TIPO);
                sqlCommand.Parameters.AddWithValue("@VEH_TRACCION", vehiculo.VEH_TRACCION);
                sqlCommand.Parameters.AddWithValue("@VEH_CANT_PASAJEROS", vehiculo.VEH_CANT_PASAJEROS);
                sqlCommand.Parameters.AddWithValue("@VEH_TRANSMISION", vehiculo.VEH_TRANSMISION);


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

            if (EliminarVehiculo(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarVehiculo(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE VEHICULO
                                                         WHERE VEH_ID = @VEH_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@VEH_ID", id);

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
