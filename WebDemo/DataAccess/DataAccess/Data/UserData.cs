using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class UserData
{
	private readonly IOracleAccess _db;
	public UserData(IOracleAccess db)
	{
		_db = db;
	}
	public Task<IEnumerable<UserModel>> GetUsers() =>
		_db.LoadData<UserModel, dynamic>("pkg_user.GetUserList", new { });
	public async Task<UserModel?> GetUser(int id)
	{
		var results = await _db.LoadData<UserModel, dynamic>("pkg_user.GetUserById", new { Id = id });
		return results.FirstOrDefault();
	}
	public Task InsertUser(UserModel user) =>
		_db.SaveData("pkg_user.InsertUser", new { user.FirstName, user.LastName });
	public Task UpdateUser(UserModel user) =>
		_db.SaveData("pkg_user.UpdateUser", user);

	public Task DeleteUser(int id) =>
		_db.SaveData("pkg_user.DeleteUser", new { Id = id });
}
