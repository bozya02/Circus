//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Circus.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnimalArtist
    {
        public int Id { get; set; }
        public Nullable<int> AnimalId { get; set; }
        public Nullable<int> ArtistId { get; set; }
    
        public virtual Animal Animal { get; set; }
        public virtual Artist Artist { get; set; }
    }
}