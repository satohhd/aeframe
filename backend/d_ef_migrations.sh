#/bin/bash


if [ $# -ne 1 ]; then
  echo "指定された引数は$#個です。" 1>&2
  echo "実行するには1個の引数が必要です。" 1>&2
  exit 1
fi
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
dotnet ef migrations add Migration_$1

