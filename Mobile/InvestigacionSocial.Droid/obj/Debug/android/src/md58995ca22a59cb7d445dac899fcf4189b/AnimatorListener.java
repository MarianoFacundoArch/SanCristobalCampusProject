package md58995ca22a59cb7d445dac899fcf4189b;


public class AnimatorListener
	extends android.animation.AnimatorListenerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationEnd:(Landroid/animation/Animator;)V:GetOnAnimationEnd_Landroid_animation_Animator_Handler\n" +
			"";
		mono.android.Runtime.register ("Lottie.Forms.Droid.AnimatorListener, Lottie.Forms.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AnimatorListener.class, __md_methods);
	}


	public AnimatorListener ()
	{
		super ();
		if (getClass () == AnimatorListener.class)
			mono.android.TypeManager.Activate ("Lottie.Forms.Droid.AnimatorListener, Lottie.Forms.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onAnimationEnd (android.animation.Animator p0)
	{
		n_onAnimationEnd (p0);
	}

	private native void n_onAnimationEnd (android.animation.Animator p0);

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