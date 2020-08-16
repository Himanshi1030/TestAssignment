Description:

A program to analyse th eOHLC at 15 seconds interval based o the trades recorded in that duration. This program calculates the OHLC at the end of the bar interval(at not at every trade) and displays the result for each bar in the console.

WorkFlow:
A console program run two threads. First Thread reads the trade input from the Json file and enqueuues the record of the subscribed stock to be passes too another FSM thread for OHLC computation. Second Thread dequeues the data until empty(FIFO). If the new trade does not fall in the current bar interval, then OHLC is calulated for the current barinterval and then the bar counter is increased by 1 to start the new bar. For testing purpose, the stock subscibe is hardcoded as websocket is not implemented yet and only top 100 bar results for that stock has been written to the console.
