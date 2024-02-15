#!/bin/bash

# Проверка на наличие аргумента
if [ "$#" -ne 1 ]; then
    echo "Ошибка: Необходимо передать один аргумент."
    exit 1
fi

parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

# Получение строки из аргумента
NEW_VERSION=$1

FILE_NAME="ProjectSettings.asset"
#FILE_NAME="text.txt"

cd ..
cd ProjectSettings/

sed -i "s/bundleVersion: [0-9]*\.[0-9]*\.[0-9]*/bundleVersion: $NEW_VERSION/g" $FILE_NAME
