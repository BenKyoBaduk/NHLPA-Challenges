namespace HockeyApi.Features {
    public class TeamModel
    {
        public TeamModel(string code, string name) {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
