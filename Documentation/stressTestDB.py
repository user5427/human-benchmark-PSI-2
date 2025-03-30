import psycopg2
from concurrent.futures import ThreadPoolExecutor
conn_str = "host=pg-37f33658-toptoptopia-0e0d.j.aivencloud.com port=24176 dbname=defaultdb user=avnadmin password=1234 sslmode=require"
def test_query():
    with psycopg2.connect(conn_str) as conn:
        conn.execute("SELECT 1")
with ThreadPoolExecutor(max_workers=10) as executor:
    for _ in range(100): executor.submit(test_query)
print("Stress test complete!")
'