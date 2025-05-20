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
