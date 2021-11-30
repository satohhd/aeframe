#/bin/bash


dotnet ef dbcontext scaffold "Host=localhost;Database=tiockdb;Username=tiockdb;Password=5970" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --table view_monthly_inventories

