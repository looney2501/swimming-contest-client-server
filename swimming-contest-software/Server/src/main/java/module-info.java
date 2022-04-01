module swimming.contest.software.Server.main {
    exports swimmingApp.service;
    exports swimmingApp.repository.dbRepository;
    requires java.sql;
    requires org.apache.logging.log4j;
    requires swimming.contest.software.Model.main;
}