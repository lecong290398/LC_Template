package crc6489467cf1bb0ec6bf;


public class SplashActivity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("XamarinSQLlite.Droid.SplashActivity, XamarinSQLlite.Android", SplashActivity.class, __md_methods);
	}


	public SplashActivity ()
	{
		super ();
		if (getClass () == SplashActivity.class)
			mono.android.TypeManager.Activate ("XamarinSQLlite.Droid.SplashActivity, XamarinSQLlite.Android", "", this, new java.lang.Object[] {  });
	}


	public SplashActivity (int p0)
	{
		super (p0);
		if (getClass () == SplashActivity.class)
			mono.android.TypeManager.Activate ("XamarinSQLlite.Droid.SplashActivity, XamarinSQLlite.Android", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
