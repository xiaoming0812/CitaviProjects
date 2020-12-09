using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SwissAcademic.Citavi;
using SwissAcademic.Citavi.Metadata;
using SwissAcademic.Citavi.Shell;
using SwissAcademic.Collections;

public static class CitaviMacro
{
	/// <summary>
	/// You can use 'Main' or 'MainAsync' it doesn`t matter.
	/// </summary>
	public static async Task MainAsync()
	{
		/// <summary>
		///  This code is an example how you can get data from citavi.
		/// </summary>
		MainForm mainForm = Program.ActiveProjectShell.PrimaryMainForm;
		List<Reference> references = mainForm.GetFilteredReferences().ToList();
		
		try
		{
			/// <summary>
			/// GenericProgressDialog is huge customizable. You can use 'RunFunc()' or 'RunAction' instead of 'RunTask()'. They are much signatures with differente parameters you can use ;)
			/// </summary>
			await GenericProgressDialog.RunTask(mainForm, DoSomeThingWithReferencesOverALongPeriodAsync, references);
		}
		catch (OperationCanceledException) 
		{
			// Here you can handle user cancelling if you use cancellationToken.ThrowIfCancellationRequested()
			DebugMacro.WriteLine("Cancelled");
			return;
		}
		catch(Exception)
		{
			// Here you can handle unexpected exceptions
			DebugMacro.WriteLine("Unexpected exception");
			return;
		}
		
		// Here you can handle stuff if the process finished
		DebugMacro.WriteLine("Completed");
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="references">This is my own parameter from above</param>
	/// <param name="progress">There are some method signatures you will get a progress object to handle the progressbar of the dialog</param>
	/// <param name="cancellationToken">There are some method signatures you will get a cancellationtoken to handle the user`s cancellation request</param>
	/// <returns></returns>
	async static Task DoSomeThingWithReferencesOverALongPeriodAsync(List<Reference> references, IProgress<PercentageAndTextProgressInfo> progress, CancellationToken cancellationToken)
	{
		for(int i=0; i < references.Count; i++)
		{
			Reference reference = references[i];
		
			// A long working method :D
			await Task.Delay(100,cancellationToken);
			
			progress.ReportSafe(reference.ShortTitle,100/references.Count*i);
		}
	}
}