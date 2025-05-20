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
instruction="
  You're assisting in preparing a detailed software architecture analysis pipeline.

This is **not a final summary** — the output you generate will be consumed by a more advanced LLM with access to our system’s internals, documentation, and requirements. That LLM will generate structured templates, detailed diagrams, and engineering outputs. Your role is to **preprocess** the raw architecture documentation.

The source material is extremely long (7000+ lines) and describes multiple architectural views of a software system: context, functional, development, information, deployment, operational, and concurrency.

For this prompt, you are focusing on **only one view** at a time (e.g., Functional View).

Your task is to:
1. Extract and **cleanly organize all relevant content** from the view with minimal summarization.
2. Group related ideas, list key components, and preserve terminology.
3. Preserve naming, descriptions, and any diagram hints (like layout, structure, or notation).
4. Remove filler and duplicative text, but **do not paraphrase heavily** — this output must remain rich for downstream processing.
5. Label any **incomplete sections**, vague statements, or areas requiring clarification for the next LLM in the chain.

Format:
- Use markdown with clear section headers.
- Use bullet points or numbered lists where appropriate.
- Do not generalize, rewrite, or simplify unless the original is unclear.
- Maintain system-specific naming and context.

Think of yourself as a cleaner preparing messy input for an expert who will do the heavy architectural modeling.

Only process the input provided — do not infer beyond it. No opinions or assumptions.

---

**Start extracting and organizing the \"[View Name]\" content below.**

This is part X of a large architecture document. Continue summarizing in the same structured format.
"

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
