#!/bin/bash

# Go through all .md files inside Sections/ recursively
find ./Sections -type f -name "*.md" ! -name "*FULL.md" | while read -r input_file; do
  base_dir=$(dirname "$input_file")
  filename=$(basename "$input_file")
  base_name="${filename%.*}"
  extension="${filename##*.}"

  # Skip already split files (those with _part_)
  if [[ "$base_name" == *"_part_"* ]]; then
    continue
  fi

  echo "Splitting: $input_file"

  # Create a unique temp prefix
  split -l 200 -d -a 3 "$input_file" "${base_dir}/${base_name}_tmp_part_"

  instruction="take this file and make it nicely formated in markdown. make sure to use the code box so the user can copy your code easily and it does not get formated by browser:"

  for tmp_file in "${base_dir}/${base_name}_tmp_part_"*; do
    final_file="${tmp_file/_tmp/}.${extension}"
    {
      echo "$instruction"
      echo
      cat "$tmp_file"
    } > "$final_file"
    rm "$tmp_file"
  done

  # Create the empty FULL file
  touch "${base_dir}/${base_name}FULL.${extension}"
done

echo "✅ All Markdown files split and FULL files created."
