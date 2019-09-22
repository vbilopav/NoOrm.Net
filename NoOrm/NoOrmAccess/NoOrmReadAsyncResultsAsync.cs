﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;


namespace NoOrm
{
    public partial class NoOrmAccess
    {
        public async Task<INoOrm> ReadAsync(string command, Func<IDictionary<string, object>, Task> results)
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = command;
                await EnsureConnectionIsOpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        await results(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
                    }

                    return this;
                }
            }
        }

        public async Task<INoOrm> ReadAsync(string command, Func<IDictionary<string, object>, Task> results, params object[] parameters)
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = command;
                await EnsureConnectionIsOpenAsync();
                cmd.AddParameters(parameters);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        await results(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
                    }

                    return this;
                }
            }
        }

        public async Task<INoOrm> ReadAsync(string command, Func<IDictionary<string, object>, Task> results, params (string name, object value)[] parameters)
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = command;
                await EnsureConnectionIsOpenAsync();
                cmd.AddParameters(parameters);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        await results(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
                    }

                    return this;
                }
            }
        }
    }
}
