#!/bin/bash

# Format apply script similar to spotlessApply for Java
# This script formats all C++ files according to .clang-format

set -e

echo "Applying C++ code formatting..."

# Find all C++ files
cpp_files=$(find . -name "*.cpp" -o -name "*.h" -o -name "*.hpp" -o -name "*.cc" -o -name "*.c" | grep -E "^./cpp/")

if [ -z "$cpp_files" ]; then
    echo "No C++ files found to format"
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
echo "Found $total_files C++ files to format"

# Apply formatting
file_count=0
formatted_files=0

for file in $cpp_files; do
    file_count=$((file_count + 1))
    echo "Formatting ($file_count/$total_files): $file"
    
    # Create backup
    cp "$file" "$file.backup"
    
    # Apply formatting
    if clang-format -i "$file"; then
        # Check if file was actually changed
        if ! cmp -s "$file" "$file.backup"; then
            formatted_files=$((formatted_files + 1))
        fi
        rm "$file.backup"
    else
        echo "Warning: Failed to format $file"
        mv "$file.backup" "$file"
    fi
done

echo ""
echo "✅ Formatting complete!"
echo "   Total files processed: $total_files"
echo "   Files modified: $formatted_files"

if [ $formatted_files -gt 0 ]; then
    echo ""
    echo "Files have been formatted. Please review the changes before committing."
fi