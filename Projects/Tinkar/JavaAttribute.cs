using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// Marks a class, method, or property as being a mirror of a
    /// java item of the indicated name.
    /// This is used to do automatic code normalization using roslyn tools.
    /// </summary>
    public class JavaAttribute : Attribute
    {
        /// <summary>
        /// Gets os sets name of java item that this is a mirror of.
        /// </summary>
        public String JavaName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaAttribute"/> class.
        /// </summary>
        /// <param name="javaName">Name of java item that this is a mirror of.</param>
        public JavaAttribute(String javaName) => this.JavaName = javaName;
    }
}
