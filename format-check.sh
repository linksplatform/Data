#!/bin/bash

# Format check script similar to spotlessCheck for Java
# This script checks if all C++ files are properly formatted

set -e

echo "Checking C++ code formatting..."

# Find all C++ files
cpp_files=$(find . -name "*.cpp" -o -name "*.h" -o -name "*.hpp" -o -name "*.cc" -o -name "*.c" | grep -E "^./cpp/")

if [ -z "$cpp_files" ]; then
    echo "No C++ files found to check"
    exit 0
fi

# Check if clang-format is available
if ! command -v clang-format &> /dev/null; then
    echo "Error: clang-format is not installed"
    echo "Please install clang-format to use this script"
    exit 1
fi

# Count total files
total_files=$(echo "$cpp_files" | wc -l)
echo "Found $total_files C++ files to check"

# Check formatting
unformatted_files=()
file_count=0

for file in $cpp_files; do
    file_count=$((file_count + 1))
    echo "Checking ($file_count/$total_files): $file"
    
    if ! clang-format --dry-run --Werror "$file" >/dev/null 2>&1; then
        unformatted_files+=("$file")
    fi
done

# Report results
if [ ${#unformatted_files[@]} -eq 0 ]; then
    echo "✅ All C++ files are properly formatted!"
    exit 0
else
    echo "❌ Found ${#unformatted_files[@]} files that need formatting:"
    printf '  %s\n' "${unformatted_files[@]}"
    echo ""
    echo "Run './format-apply.sh' to fix formatting issues"
    exit 1
fi