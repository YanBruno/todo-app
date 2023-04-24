using Dapper;
using System.Data;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Repositories;
using TodoApp.Infra.Src.DataContext;
using TodoApp.Infra.Src.QueryResults;
using TodoApp.Infra.Src.SqliteUtils;

namespace TodoApp.Infra.Src.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(TodoAppContext context) : base(context) { }

    public async Task<bool> CheckEmail(string emailAddress)
    {
        var result = await Context
            .Connection
            .QueryFirstAsync<int>(
                "select count(1) from tb_user where Email = @emailAddress"
                , new { emailAddress }
                , commandType: CommandType.Text
            );

        if (result > 0)
        {
            return true;
        }
        else { return false; }

    }

    public async Task<bool> CheckExists(Guid entityId)
    {
        var result = await Context
            .Connection
            .QueryFirstAsync<int>(
                "select count(1) from tb_user where UserId = @entityId"
                , new { entityId }
                , commandType: CommandType.Text
            );

        if (result > 0)
        {
            return true;
        }
        else { return false; }
    }

    public async Task<bool> CreateAsync(User entity)
    {
        try
        {
            await Context
                .Connection
                .ExecuteAsync(Scripts.InsertUser, new
                {
                    @UserId = entity.Id.ToString(),
                    @createdAt = entity.CreatedAt,
                    @firstName = entity.Name.FirstName,
                    @lastName = entity.Name.LastName,
                    @email = entity.Email.Address,
                    @hash = entity.GetPassword().Hash,
                    @active = entity.Active
                }
                , commandType: CommandType.Text
            );

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(User entity)
    {
        //todo retornar a listas junto com o user
        try
        {
            var result = await Context
                .Connection
                .QueryFirstOrDefaultAsync<UserQueryResult>(
                    "delete from tb_user where UserId = @entityId"
                    , new { entityId = entity.Id.ToString() }
                    , commandType: CommandType.Text
            ); ;

            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public async Task<User?> GetAsync(string emailAddress)
    {
        //todo retornar a listas junto com o user
        try
        {
            var result = await Context
                .Connection
                .QueryFirstOrDefaultAsync<UserQueryResult>(
                    "select * from tb_user where Email = @emailAddress"
                    , new { emailAddress }
                    , commandType: CommandType.Text
            );

            return result?.Build();

        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<User?> GetAsync(Guid entityId)
    {
        //todo retornar a listas junto com o user
        try
        {
            var result = await Context
                .Connection
                .QueryFirstOrDefaultAsync<UserQueryResult>(
                    "select * from tb_user where UserId = @entityId"
                    , new { entityId = entityId.ToString() }
                    , commandType: CommandType.Text
            );

            return result?.Build();

        }
        catch (Exception)
        {
            return null;
        }
    }

    public Task<bool> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}