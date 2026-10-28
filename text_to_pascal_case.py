def convert(text,separator=","):
    parts=text.split(separator)
    for i in range(len(parts)):
        words=parts[i].split("_")
        for j in range(len(words)):
            words[j]=words[j].title()
        parts[i]="".join(words)
    print(separator.join(parts))

convert("order_id,product_id,quantity,unit_price,discount_percentage,discount_amount,gross_sales,tax_amount,shipping_cost,net_sales,product_cost,profit")