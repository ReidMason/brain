import os
from datetime import datetime


def ensure_path_exists(directory_path: str) -> None:
    """ If the supplied path doesn't exist this method will recursively create it

    :param directory_path: The path you want to create
    :return: None
    """
    if not os.path.exists(directory_path):
        os.makedirs(directory_path)


def js_format_datetime(date: datetime) -> str:
    """ Formats a datetime object to a string supported by javascript

    :param date: Datetime object to be parsed
    :return: A string in javascript supported date format
    """
    return date.strftime('%Y-%m-%dT%H:%M:%SZ')
