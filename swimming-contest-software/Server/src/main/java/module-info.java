module swimming.contest.software.Server.main {
    exports server.service;
    exports server.repository.dbRepository;
    requires java.sql;
    requires org.apache.logging.log4j;
    requires swimming.contest.software.Model.main;
}