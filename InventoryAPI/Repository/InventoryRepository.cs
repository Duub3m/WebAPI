using System.Text;
using InventoryAPI.Models;
using MySqlConnector;

public class InventoryRepository{


private string connectionString;
  
  public InventoryRepository(string connectionString){

    this.connectionString = connectionString;
  }

  public List<User> getAllUsers(){
    List<User> users = new List<User>();
    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
    MySqlCommand mySqlCommand= new MySqlCommand(); //SQL COmmand for executing
    try{
      mySqlConnection.Open();//Opens Database Connection
      mySqlCommand.Connection=mySqlConnection;
      mySqlCommand.CommandText="SELECT * FROM User;";
      MySqlDataReader reader = mySqlCommand.ExecuteReader();
      while(reader.Read()){
        User user = getUserFromReader(reader);
        users.Add(user);
      }
    } catch(Exception ex){ //Catch any exception errors
      Console.WriteLine(ex.Message);
    } 
    finally{
      mySqlConnection.Close();
    }
    return users;
}

public User getUser(int userId){
    User user = null!;

    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
    MySqlCommand mySqlCommand= new MySqlCommand(); //SQL COmmand for executing
    try{
      mySqlConnection.Open();//Opens Database Connection
      mySqlCommand.Connection=mySqlConnection;
      mySqlCommand.CommandText="SELECT * FROM TestTable.User WHERE UserId =" + userId + ";";
      MySqlDataReader reader = mySqlCommand.ExecuteReader();
      
      while(reader.Read()){
        user = getUserFromReader(reader);
        return user;
      }
    } catch(Exception ex){ //Catch any exception errors
      Console.WriteLine(ex.Message);
    } 
    finally{
      mySqlConnection.Close();
    }
    return user;
}


public bool addUsers(User user){
   MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
   MySqlCommand mySqlCommand= new MySqlCommand(); //SQL COmmand for executing
   try{
    mySqlConnection.Open();
    mySqlCommand.Connection=mySqlConnection;
    mySqlCommand.CommandText = "INSERT INTO User VALUES" + "(" + user.UserId + ", " + "\"" + user.FirstName + "\", " + "\"" + user.LastName + "\"" + ");";
    var affectedRows = mySqlCommand.ExecuteNonQuery();
    if (affectedRows == 1){
     return true;
    }
      return false;
    
   } catch(Exception ex){
    Console.WriteLine(ex.Message);
    return false;
   }
   finally{
    mySqlConnection.Close();
   }
}

public bool deleteUsers(int userId){
   MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
   MySqlCommand mySqlCommand= new MySqlCommand(); //SQL COmmand for executing
   try{
    mySqlConnection.Open();
    mySqlCommand.Connection=mySqlConnection;
    mySqlCommand.CommandText = "DELETE FROM User WHERE UserId=" + "'" + userId + "';";
    var affectedRows = mySqlCommand.ExecuteNonQuery();
    if(affectedRows == 1){
      return true;
    }
    return false;
   } catch(Exception ex){
    Console.WriteLine(ex.Message);
    return false;
   }
   finally{
    mySqlConnection.Close();
   }
}

public User getUserFromReader(MySqlDataReader reader){
  User user = new User();
  user.UserId = Int32.Parse(reader["UserId"].ToString());
  user.FirstName = reader["FirstName"].ToString();
  user.LastName = reader["LastName"].ToString();

  return user;
}

}

