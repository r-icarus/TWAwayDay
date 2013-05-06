using System;
using System.Collections.Generic;
using System.Linq;

using CodePhile;
using CodePhile.Terminal;

namespace SpaceBeans {
    internal class FixedLabelSequence : LabelSequence {

        private readonly string[] labels;

        public FixedLabelSequence(params string[] labels) {
            this.labels = labels;
        }

        public override IEnumerable<string> GetLabels() {
            return labels;
        }

        public override int ParseLabel(string label) {
            return labels.IndexOf(label, StringComparer.InvariantCultureIgnoreCase);
        }

        public override string Pattern {
            get { return string.Join("|", labels.Select(l => "(" + l + ")")); }
        }
    }
}