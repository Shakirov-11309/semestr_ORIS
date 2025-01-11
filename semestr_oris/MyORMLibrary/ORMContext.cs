﻿using Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using TemplateEngine.Models;

namespace MyORMLibrary;
public class ORMContext<T> where T : class, new()
{
    private readonly IDbConnection _dbConnection;

    public ORMContext(IDbConnection dbconnection)
    {
        _dbConnection = dbconnection;
    }

    public void Create(T entity)
    {
        var properties = entity.GetType().GetProperties()
            .Where(p => p.Name != "Id")
            .ToList();

        var columns = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => "@" + p.Name));

        var query = $"INSERT INTO {typeof(T).Name}s ({columns}) VALUES ({values})";

        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;

            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@" + property.Name;
                parameter.Value = property.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            if (_dbConnection.State == ConnectionState.Open) 
            {
                _dbConnection.Open();
            }
            command.ExecuteNonQuery();
            if (_dbConnection.State == ConnectionState.Closed) 
            {
                _dbConnection.Close();
            }
        }
    }

    public T ReadById(int? id)
    {
        var tableName = typeof(T).Name;
        using (var connection = _dbConnection)
        {
            if(connection.State != ConnectionState.Open) connection.Open();
            string queryRequest = $"SELECT * FROM {tableName} WHERE id = @id";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;

                var parametr = command.CreateParameter();
                parametr.ParameterName = "@id";
                parametr.Value = id;
                command.Parameters.Add(parametr);


                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Map(reader);
                    }
                }
            }
        }
        return null;
    }

    private T Map(IDataReader reader)
    {
        var entity = new T();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
            {
                property.SetValue(entity, reader[property.Name]);
            }
        }
        return entity;
    }
    public T ReadByAll<T>() where T : class, new()
    {
        var tableName = typeof(T).Name;
        using (var connection = _dbConnection)
        {
            connection.Open();
            string queryRequest = $"SELECT * FROM {tableName}";
            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToEntity<T>(reader);
                    }
                }
                _dbConnection.Close();
            }
        }

        return null;
    }

    public List<string> GetPosterUrl() 
    {
        var result = new List<string>();
        string query = "SELECT poster_url FROM movies";
        using (var command = _dbConnection.CreateCommand()) 
        {
            command.CommandText = query;
            _dbConnection.Open();
            using (var reader = command.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    result.Add(reader.GetString(0));
                }
            }
            _dbConnection.Close();
        }
        return result;
    }

    public T CheckUserByData(string email)
    {
        var tableName = typeof(T).Name;
        using (var connection = _dbConnection)
        {
            connection.Open();
            string queryRequest = $"SELECT * FROM {tableName} WHERE email = @email ";
            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;
                var parametr = command.CreateParameter();
                parametr.ParameterName= "@email";
                parametr.Value = email;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Map(reader);
                    }
                }
            }
        }
        return null;
    }

    public T ReadByName<T>(string Name) where T : class, new()
    {
        var tableName = typeof(T).Name;
        using (var connection = _dbConnection)
        {
            connection.Open();
            string queryRequest = $"SELECT * FROM {tableName} WHERE name = @Name ";
            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@name";
                parameter.Value = Name;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToEntity<T>(reader);
                    }
                }
            }
        }

        return null;
    }

    public void Update(int id, T entity)
    {
        var properties = typeof(T).GetProperties()
            .Where(p => p.Name != "Id")
            .ToList();

        var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
        var query = $"UPDATE {typeof(T).Name}s SET {setClause} WHERE Id = @id";

        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;

            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@" + property.Name;
                parameter.Value = property.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);

            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }

    public void Delete(int id)
    {
        var query = $"DELETE FROM {typeof(T).Name}s WHERE Id = @id";
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            var parametr = command.CreateParameter();
            parametr.ParameterName = "@id";
            parametr.Value = id;
            command.Parameters.Add(parametr);

            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();
        }
    }

    private T MapToEntity<T>(IDataReader reader) where T : class, new()
    {
        T entity = new T();
        Type entityType = typeof(T);
        PropertyInfo[] properties = entityType.GetProperties();

        //Получаем схему таблицы для проверки существования столбцов
        DataTable schemaTable = reader.GetSchemaTable();

        foreach (PropertyInfo property in properties)
        {
            string columnName = property.Name;
            //Проверяем наличие столбца в схеме
            DataRow[] rows = schemaTable.Select($"ColumnName = '" + columnName + "'");
            if (rows.Length > 0)
            {
                try
                {
                    int ordinal = reader.GetOrdinal(columnName);
                    object value = reader.GetValue(ordinal);
                    if (value != DBNull.Value)
                    {
                        property.SetValue(entity, Convert.ChangeType(value, property.PropertyType));
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    //Обработка ситуации, когда столбец неожиданно пропал
                    Console.WriteLine($"Column '{columnName}' not found in result set.");
                }
            }
        }
        return entity;
    }

    public T FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        var sqlQuery = BuildSqlQuery(predicate, singleResult: true);
        return ExecuteQuerySingle(sqlQuery);
    }

    public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
    {
        var sqlQuery = BuildSqlQuery(predicate, singleResult: false);
        return ExecuteQueryMultiple(sqlQuery);
    }

    private T ExecuteQuerySingle(string query)
    {
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Map(reader);
                }
            }
            _dbConnection.Close();
        }
        return null;
    }

    private IEnumerable<T> ExecuteQueryMultiple(string query)
    {
        var results = new List<T>();
        using (var command = _dbConnection.CreateCommand())
        {
            command.CommandText = query;
            _dbConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(Map(reader));
                }
            }
            _dbConnection.Close();
        }
        return results;
    }

   private string BuildWhereClause(Expression expression, Dictionary<string, object> parameters)
   {
        if (expression is BinaryExpression binaryExpression)
        {
            var left = BuildWhereClause(binaryExpression.Left, parameters);
            var right = BuildWhereClause(binaryExpression.Right, parameters);
            var operatorString = binaryExpression.NodeType switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.NotEqual => "<>",
                ExpressionType.GreaterThan => ">",
                ExpressionType.GreaterThanOrEqual => ">=",
                ExpressionType.LessThan => "<",
                ExpressionType.LessThanOrEqual => "<=",
                ExpressionType.AndAlso => "AND",
                ExpressionType.OrElse => "OR",
                _ => throw new NotSupportedException($"Operator {binaryExpression.NodeType} is not supported")
            };
            return $"{left} {operatorString} {right}";
        }
        else if (expression is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        else if (expression is ConstantExpression constantExpression)
        {
            var parameterName = $"@p{parameters.Count}";
            parameters.Add(parameterName, constantExpression.Value);
            return parameterName;
        }
        else
        {
            throw new NotSupportedException($"Expression type {expression.GetType().Name} is not supported");
        }
   }

    private string ParseExpression(Expression expression)
    {
        if (expression is BinaryExpression binary)
        {
            // разбираем выражение на составляющие
            var left = ParseExpression(binary.Left);  // Левая часть выражения
            var right = ParseExpression(binary.Right); // Правая часть выражения
            var op = GetSqlOperator(binary.NodeType);  // Оператор (например, > или =)
            return $"({left} {op} {right})";
        }
        else if (expression is MemberExpression member)
        {
            return member.Member.Name; // Название свойства
        }
        else if (expression is ConstantExpression constant)
        {
            return FormatConstant(constant.Value); // Значение константы
        }
        throw new NotSupportedException($"Unsupported expression type: {expression.GetType().Name}");
    }

    private string GetSqlOperator(ExpressionType nodeType)
    {
        return nodeType switch
        {
            ExpressionType.Equal => "=",
            ExpressionType.AndAlso => "AND",
            ExpressionType.NotEqual => "<>",
            ExpressionType.GreaterThan => ">",
            ExpressionType.LessThan => "<",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThanOrEqual => "<=",
            _ => throw new NotSupportedException($"Unsupported node type: {nodeType}")
        };
    }

    private string FormatConstant(object value)
    {
        return value is string ? $"'{value}'" : value.ToString();
    }

    private string BuildSqlQuery(Expression<Func<T, bool>> predicate, bool singleResult)
    {
        var tableName = typeof(T).Name + "s"; // Имя таблицы, основанное на имени класса
        var whereClause = ParseExpression(predicate.Body);

        return $"SELECT * FROM {tableName} WHERE {whereClause}".Trim();
    }

    public List<Movies> GetMovies() 
    {
        var result = new List<Movies>();
        string query = "SELECT id, card_url, poster_url, amediateka_rating, rating, release_year, description_card, title, bg_url, genre FROM movies";
        using (var command = _dbConnection.CreateCommand()) 
        {
            command.CommandText = query;
            _dbConnection.Open();
            using(var reader = command.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    var movies = new Movies
                    {
                        Id = reader.GetInt32(0),
                        card_url = reader.GetString(1),
                        poster_url = reader.GetString(2),
                        amediateka_rating = reader.GetDouble(3),
                        rating = reader.GetDouble(4),
                        release_year = reader.GetInt32(5),
                        description_card = reader.GetString(6),
                        title = reader.GetString(7),
                        bg_url = reader.GetString(8),
                        genre = reader.GetString(9),

                    };
                    result.Add(movies);
                }
            }
            _dbConnection.Close();
        }
        return result;
    }

    public TempleteData ReadActorsByMovieId(int movieId)
    {
        using (var connection = _dbConnection)
        {
            if (connection.State != ConnectionState.Open) connection.Open();
            string queryRequest = @"
                SELECT a.id, a.name
                FROM movie_staff ms
                INNER JOIN actors a ON ms.actor_id = a.id
                WHERE ms.movie_id = @movieId";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@movieId";
                parameter.Value = movieId;
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TempleteData
                        {
                            id = reader.GetInt32(0), // id
                            name = reader.GetString(1) // name
                        };
                    }
                }
            }
        }
        return null;
    }

    public TempleteData ReadDirectorsByMovieId(int movieId)
    {
        using (var connection = _dbConnection)
        {
            if (connection.State != ConnectionState.Open) connection.Open();
            string queryRequest = @"
                SELECT a.id, a.name
                FROM movie_staff ms
                INNER JOIN directors a ON ms.actor_id = a.id
                WHERE ms.movie_id = @movieId";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@movieId";
                parameter.Value = movieId;
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TempleteData
                        {
                            id = reader.GetInt32(0), // id
                            name = reader.GetString(1) // name
                        };
                    }
                }
            }
        }
        return null;
    }

    public TempleteData ReadCountriesByMovieId(int movieId)
    {
        using (var connection = _dbConnection)
        {
            if (connection.State != ConnectionState.Open) connection.Open();
            string queryRequest = @"
                SELECT c.id, c.name
                FROM movie_countries mc
                INNER JOIN countries c ON mc.country_id = c.id
                WHERE mc.movie_id = @movieId;";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@movieId";
                parameter.Value = movieId;
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TempleteData
                        {
                            id = reader.GetInt32(0), // id
                            name = reader.GetString(1) // name
                        };
                    }
                }
            }
        }
        return null;
    }

    public TempleteData ReadGenresByMovieId(int movieId)
    {
        using (var connection = _dbConnection)
        {
            if (connection.State != ConnectionState.Open) connection.Open();
            string queryRequest = @"
                SELECT c.id, c.name
                FROM movie_genres mc
                INNER JOIN genres c ON mc.genre_id = c.id
                WHERE mc.movie_id = @movieId;";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = queryRequest;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@movieId";
                parameter.Value = movieId;
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TempleteData
                        {
                            id = reader.GetInt32(0), // id
                            name = reader.GetString(1) // name
                        };
                    }
                }
            }
        }
        return null;
    }
}