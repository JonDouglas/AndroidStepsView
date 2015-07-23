using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Anton46.Stepsview;

namespace AndroidStepsViewSample
{
    [Activity(Label = "AndroidStepsViewSample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private List<string> views = new List<string> {"View 1", "View 2", "View 3"};

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ListView listview = FindViewById<ListView>(Resource.Id.list);

            MyAdapter adapter = new MyAdapter(this, 0);
            adapter.AddAll(views);
            listview.Adapter = adapter;
        }
    }

    public class MyAdapter : ArrayAdapter<string>
    {
        private string[] labels = {"Step 1", "Step 2", "Step 3", "Step 4", "Step 5"};
        public MyAdapter(Context context, int resource) : base(context, resource)
        {
            
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder;
            if (convertView == null)
            {
                convertView = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.row, null);
                holder = new ViewHolder(convertView);
                convertView.Tag = holder;
            }
            else
            {
                holder = (ViewHolder) convertView.Tag;
            }

            holder.label.Text = GetItem(position);

            holder.stepsView.SetCompletedPosition(position % labels.Length)
                .SetLabels(labels)
                .SetBarColorIndicator(Context.Resources.GetColor(Resource.Color.blue))
                .SetProgressColorIndicator(Context.Resources.GetColor(Resource.Color.orange))
                .SetLabelColorIndicator(Context.Resources.GetColor(Resource.Color.orange))
                .DrawView();

            return convertView;
        }

        class ViewHolder : View
        {
            public TextView label;
            public StepsView stepsView;

            public ViewHolder(View view) : base(view.Context)
            {
                label = view.FindViewById<TextView>(Resource.Id.label);
                stepsView = view.FindViewById<StepsView>(Resource.Id.stepsView);
            }

        }
    }
}

