1. Run SignalRBroadCasterDB_Script from SQL Server Management Studio.
2. Make sure a database name called SignalRBroadCasterDB is created.
3. Make sure a table name called ConfigurationLookUp is created and has three records/rows.
4. Make sure ConfigurationLookUp has three triggers. Namely, TRG_INSERT_CONFIGURATION_LOOKUP, TRG_DELETE_CONFIGURATION_LOOKUP, TRG_UPDATE_CONFIGURATION_LOOKUP
5. Make sure the database has a store procedure named USP_INVOKE_REST_SERVICE.
6. Build the entire solution.
7. Run RESTfulSignalRService project in browser using IIS Express. 
8. Open one of the message listener sample, MessageListenerWPFApp exe from it folder.
9. Open Configuration LookUp table for edit using SQL Server Management Studio.
10. Update few records and observer the effect.

For more see MessageListernerWPFApp_SignalRBroadCasterDB.gif in browser.

Wonde Tadesse.

Enjoy! 
