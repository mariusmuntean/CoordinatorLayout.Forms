namespace CoordinatorLayout.XamarinForms
{
    /// <summary>
    /// The expansion state of the top view when the control is started
    /// </summary>
    public enum InitialExpansionState
    {
        /// <summary>
        /// The top view is collapsed
        /// </summary>
        Collapsed,
            
        /// <summary>
        /// The top view is expanded
        /// </summary>
        Expanded,
            
        /// <summary>
        /// Not implemented right now, but I'm thinking to fire a few events based on the expansion state
        /// </summary>
        Other
    }
}