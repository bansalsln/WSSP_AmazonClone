using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL.Implementation
{
    //public class SQL_Implementation : Contract.DAL_Contract
    //{
    //    private string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        

    //    public void InsertPacket(Packet p)
    //    {
    //        string queryString1 = "INSERT into AndroidWearData (user_id, time_stamp, accelerometer_x, accelerometer_y, accelerometer_z, gyroscope_x,gyroscope_y, gyroscope_z, heart_rate) " 
    //                                 + "values(@USER_ID, @TIMESTAMP, @A_X, @A_Y, @A_Z, @G_X, @G_Y, @G_Z, @HEARTRATE)";

    //        string queryString2 = "INSERT into VehicleData (user_id, time_stamp,steering_wheel_angle,torque_at_transmission,engine_speed,vehicle_speed,accelerator_pedal_position,brake_pedal_status,transmission_gear_position,odometer,ignition_status,fuel_level,fuel_consumed_since_restart,headlamp_status,high_beam_status,windshield_wiper_status) "
    //                                + "values(@USER_ID, @TIMESTAMP, @STEERING, @TORQUE, @E_SPEED, @V_SPEED, @A_PEDAL, @B_PEDAL, @GEAR, @ODO, @IGNITION, @FUEL, @FUEL_CONSUMED, @HEADLAMP, @HIGH_BEAM, @WIPER)";

    //        SqlConnection conn = new SqlConnection(ConnectionString);
    //        SqlTransaction tran = null;

    //        try
    //        {
    //            conn.Open();
    //            tran = conn.BeginTransaction();
    //            SqlCommand cmd;

    //            int x = 0;
    //            foreach (var item in p.AndroidWearData)
    //            {
    //                cmd = conn.CreateCommand();
    //                cmd.CommandType = CommandType.Text;
    //                cmd.Transaction = tran;
    //                cmd.CommandText = queryString1;

    //                cmd.Parameters.AddWithValue("@USER_ID", p.user_id);
    //                cmd.Parameters.AddRange(GetParametersByData(item));

    //                x += cmd.ExecuteNonQuery();
    //            }

    //            int y = 0;
    //            foreach (var item in p.VehicleData)
    //            {
    //                cmd = conn.CreateCommand();
    //                cmd.CommandType = CommandType.Text;
    //                cmd.Transaction = tran;
    //                cmd.CommandText = queryString2;

    //                cmd.Parameters.AddWithValue("@USER_ID", p.user_id);
    //                cmd.Parameters.AddRange(GetParametersByData(item));

    //                y += cmd.ExecuteNonQuery();
    //            }
                
    //            if(x == p.AndroidWearData.Length && y == p.VehicleData.Length)
    //            {
    //                tran.Commit();
    //            }
    //            else
    //            {
    //                throw new Exception("Transaction Rolled back");
    //            }
    //        }
    //        catch (Exception E)
    //        {
    //            if(tran != null)
    //                tran.Rollback();
    //            throw E;
    //        }
    //        finally
    //        {
    //            if(conn.State == ConnectionState.Open)
    //                conn.Close();
    //        }
    //    }

    //    public IEnumerable<AndroidWearData> RetrieveAndroidWearData(string user_id)
    //    {
    //        //(user_id, time_stamp, accelerometer_x, accelerometer_y, accelerometer_z, gyroscope_x, gyroscope_y, gyroscope_z, heart_rate)
    //        List <AndroidWearData> records = new List<AndroidWearData>();

    //        string queryString = "SELECT * from AndroidWearData where user_id = @USER_ID";

    //        SqlConnection conn = new SqlConnection(ConnectionString);
    //        conn.Open();

    //        SqlCommand cmd = conn.CreateCommand();
    //        cmd.CommandType = CommandType.Text;
    //        cmd.CommandText = queryString;
    //        cmd.Parameters.AddWithValue("@USER_ID", user_id);

    //        try
    //        {
    //            SqlDataReader reader = cmd.ExecuteReader();
    //            while(reader.Read())
    //            {
    //                records.Add(new AndroidWearData()
    //                {
    //                    TimeStamp = (double)reader["time_stamp"],
    //                    Accelerometer_X = (double)reader["accelerometer_x"],
    //                    Accelerometer_Y = (double)reader["accelerometer_y"],
    //                    Accelerometer_Z = (double)reader["accelerometer_z"],
    //                    Gyroscope_X = (double)reader["gyroscope_x"],
    //                    Gyroscope_Y = (double)reader["gyroscope_y"],
    //                    Gyroscope_Z = (double)reader["gyroscope_z"],
    //                    HeartRate = (double)reader["heart_rate"]
    //                });
    //            }


    //        }
    //        catch(Exception E)
    //        {
    //            throw E;
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        return records;
    //    }


    //    /*
    //     * Helper Methods 
    //     * */

    //    private SqlParameter[] GetParametersByData(AndroidWearData data)
    //    {
    //        SqlParameter[] parameters = new SqlParameter[] {
    //            new SqlParameter("@TIMESTAMP", data.TimeStamp),
    //            new SqlParameter("@A_X", data.Accelerometer_X),
    //            new SqlParameter("@A_Y", data.Accelerometer_Y),
    //            new SqlParameter("@A_Z", data.Accelerometer_Z),
    //            new SqlParameter("@G_X", data.Gyroscope_X),
    //            new SqlParameter("@G_Y", data.Gyroscope_Y),
    //            new SqlParameter("@G_Z", data.Gyroscope_Z),
    //            new SqlParameter("@HEARTRATE", data.HeartRate),
    //        };
    //        return parameters;
    //    }

    //    private SqlParameter[] GetParametersByData(VehicleData data)
    //    {
    //        SqlParameter[] parameters = new SqlParameter[] {
    //            new SqlParameter("@TIMESTAMP", data.TimeStamp),
    //            new SqlParameter("@STEERING", data.SteeringWheelAngle),
    //            new SqlParameter("@TORQUE", data.TorqueAtTransmission),
    //            new SqlParameter("@E_SPEED", data.EngineSpeed),
    //            new SqlParameter("@V_SPEED", data.VehicleSpeed),
    //            new SqlParameter("@A_PEDAL", data.AcceleratorPedalPosition),
    //            new SqlParameter("@B_PEDAL", data.BrakePedalStatus),
    //            new SqlParameter("@GEAR", data.TransmissionGearPosition),
    //            new SqlParameter("@ODO", data.Odometer),
    //            new SqlParameter("@IGNITION", data.IgnitionStatus),
    //            new SqlParameter("@FUEL", data.FuelLevel),
    //            new SqlParameter("@FUEL_CONSUMED", data.FuelConsumedSinceRestart),
    //            new SqlParameter("@HEADLAMP", data.HeadlampStatus),
    //            new SqlParameter("@HIGH_BEAM", data.HighBeamStatus),
    //            new SqlParameter("@WIPER", data.WindshieldWiperStatus),
    //        };
    //        return parameters;

    //    }
    //}
}
