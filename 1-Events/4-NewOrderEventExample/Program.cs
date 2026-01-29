using System;
using System.Collections.Generic;

public class OrderEventArgs:EventArgs
{
    public int OrderID { get; }
    public int OrderTotalPrice { get; }
    public string ClientEmail { get; }

    public OrderEventArgs(int OrderID, int OrderTotalPrice, string ClientEmail)
    {
        this.OrderID = OrderID;
        this.OrderTotalPrice = OrderTotalPrice;
        this.ClientEmail = ClientEmail;
    }
}

public class Order
{
    public event EventHandler<OrderEventArgs> OnOrderCreated;

    public void Create(int OrderID, int OrderTotalPrice, string ClientEmail)
    {
        Console.WriteLine("New Order created; now will notify everyone by raising the event.\n");

        if (OnOrderCreated != null)
            OnOrderCreated(this,new OrderEventArgs(OrderID, OrderTotalPrice, ClientEmail));
    }
}

public class EmailService
{
    public void Subscribe(Order order)
    {
        order.OnOrderCreated += HandleNewOrder;
    }
    public void UnSubscribe(Order order)
    {
        order.OnOrderCreated -= HandleNewOrder;
    }
    public void HandleNewOrder(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"----------Email Service--------");
        Console.WriteLine($"Email Service Object Received a new order event");
        Console.WriteLine($"Order ID: {e.OrderID}");
        Console.WriteLine($"Orider Price: {e.OrderTotalPrice}");
        Console.WriteLine($"Email: {e.ClientEmail}");
        Console.WriteLine($"\nSend an email");
        Console.WriteLine($"--------------------------------");
        /*
         here you write the code to send the email to the client 
         */
        Console.WriteLine();
    }
}

public class SMSService
{
    public void Subscribe(Order order)
    {
        order.OnOrderCreated += HandleNewOrder;
    }

    public void UnSubscribe(Order order)
    {
        order.OnOrderCreated -= HandleNewOrder;
    }

    public void HandleNewOrder(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"------------SMS Service--------");
        Console.WriteLine($"SMS Service Object Received a new order event");
        Console.WriteLine($"Order ID: {e.OrderID}");
        Console.WriteLine($"Orider Price: {e.OrderTotalPrice}");
        Console.WriteLine($"Email: {e.ClientEmail}");
        Console.WriteLine($"\nSend SMS");
        Console.WriteLine($"--------------------------------");
        /*
         here you write the code to send the SMS to the client 
         */
        Console.WriteLine();
    }
}

public class ShippingService
{
    public void Subscribe(Order order)
    {
        order.OnOrderCreated += HandleNewOrder;
    }

    public void UnSubscribe(Order order)
    {
        order.OnOrderCreated -= HandleNewOrder;
    }

    public void HandleNewOrder(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"---------Shipping Service-------");
        Console.WriteLine($"Shipping Service Object Received a new order event");
        Console.WriteLine($"Order ID: {e.OrderID}");
        Console.WriteLine($"Orider Price: {e.OrderTotalPrice}");
        Console.WriteLine($"Email: {e.ClientEmail}");
        Console.WriteLine($"\nHandel Shipping");
        Console.WriteLine($"--------------------------------");
        /*
         here you write the code to handel shipping to the client 
         */
        Console.WriteLine();
    }
}

public class Program
{
    static void Main()
    {
        var order = new Order();

        EmailService emailService = new EmailService();
        SMSService smsService = new SMSService();
        ShippingService shippingService = new ShippingService();

        emailService.Subscribe(order);
        smsService.Subscribe(order);
        shippingService.Subscribe(order);

        order.Create(10, 540, "Ahmed@gmail.com");

        shippingService.UnSubscribe(order);
        order.Create(11, 300, "Ali@gmail.com");

        Console.ReadLine();
    }
}


