namespace Model
{
    /// <summary>
    /// 托管组件，逻辑在TrusteeshipComponentSystem扩展
    /// </summary>
    public class TrusteeshipComponent : Component
    {
        public bool Playing { get; set; }

        public override void Dispose()
        {
            if(this.Id == 0)
            {
                return;
            }

            base.Dispose();

            this.Playing = false;
        }
    }
}
