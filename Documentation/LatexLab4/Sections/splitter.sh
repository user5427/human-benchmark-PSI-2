#!/bin/bash

# Check if the input file is provided
if [ -z "$1" ]; then
  echo "Usage: $0 filename.md"
  exit 1
fi

input_file="$1"

# Verify that the file exists
if [ ! -f "$input_file" ]; then
  echo "Error: File '$input_file' not found."
  exit 1
fi

# Extract base name and extension
base_name="${input_file%.*}"
extension="${input_file##*.}"

# Temporary split files (no extension)
split -l 200 -d -a 3 "$input_file" "${base_name}_tmp_part_"

# Instruction to prepend
instruction="take this file and make it nicely formated in markdown. make sure to use the code box so the user can copy your code easily and it does not get formated by browser:"

# Loop through split files, add instruction, rename
for tmp_file in ${base_name}_tmp_part_*; do
  final_file="${tmp_file/_tmp/}.${extension}"
  {
    echo "$instruction"
    echo
    cat "$tmp_file"
  } > "$final_file"
  rm "$tmp_file"
done

# Create an empty FULL file
touch "${base_name}FULL.${extension}"

echo "All tasks complete. Created:"
ls "${base_name}_part_"*.${extension} "${base_name}FULL.${extension}"
