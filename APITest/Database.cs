using System;
using System.Data.SqlClient;

public class DB
{

	public bool CheckExists(int userID)
	{
		string connectionString = "Data Source=DESKTOP-IBERPBJ;Initial Catalog=TEST;Integrated Security=True;Encrypt=False";

		string move = "SELECT COUNT(*) from SilknetUsers WHERE CustomerID = @userid";

		using (SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();

			using (SqlCommand selectUser =  new SqlCommand(move,conn))
			{
				selectUser.Parameters.AddWithValue("@userid", userID);

				int userCount = (int)selectUser.ExecuteScalar();
				
				if(userCount > 0)
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




}