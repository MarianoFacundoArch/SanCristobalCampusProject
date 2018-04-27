package md53ce643dd8eccd9a8e37e894e8722ff9d;


public class LongPressEffect_OnLongLickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLongClick:(Landroid/view/View;)Z:GetOnLongClick_Landroid_view_View_Handler:Android.Views.View/IOnLongClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("UXDivers.Artina.Shared.LongPressEffect+OnLongLickListener, UXDivers.Artina.Shared.Droid, Version=2.6.9.0, Culture=neutral, PublicKeyToken=null", LongPressEffect_OnLongLickListener.class, __md_methods);
	}


	public LongPressEffect_OnLongLickListener ()
	{
		super ();
		if (getClass () == LongPressEffect_OnLongLickListener.class)
			mono.android.TypeManager.Activate ("UXDivers.Artina.Shared.LongPressEffect+OnLongLickListener, UXDivers.Artina.Shared.Droid, Version=2.6.9.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onLongClick (android.view.View p0)
	{
		return n_onLongClick (p0);
	}

	private native boolean n_onLongClick (android.view.View p0);

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
