#/bin/bash
if [ $# -ne 2 ]; then
  echo "指定された引数は$#個です。" 1>&2
  echo "実行するには2個の引数が必要です。" 1>&2
  exit 1
fi
dotnet aspnet-codegenerator controller -name $1Controller -async -api -m $2 -dc ApplicationDbContext -outDir Controllers

